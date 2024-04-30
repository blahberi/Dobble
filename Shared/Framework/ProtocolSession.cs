using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	internal class ProtocolSession<TConnectionContext> : IProtocolSession, IRequestManager where TConnectionContext : ConnectionContext, new()
	{
		private readonly IControllerFactory<TConnectionContext> controllerFactory;
		private readonly TcpClient tcpClient;
		private readonly TConnectionContext connectionContext;
		private readonly NetworkStream stream;
		private readonly StreamWriter writer;
		private readonly StreamReader reader;
		private readonly JavaScriptSerializer serializer;
		private readonly Dictionary<Guid, TaskCompletionSource<string>> pendingClientRequests;
		private readonly Dictionary<Guid, CancellationTokenSource> pendingHandledRequests;
		private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);  // Initial count is 1, meaning one concurrent access allowed.
		private Task messageLoop;

		public ProtocolSession(IControllerFactory<TConnectionContext> controllerFactory, IServiceLocator serviceLocator, TcpClient tcpClient)
		{
			this.connectionContext = new TConnectionContext();
			this.connectionContext.RequestManager = this;
			this.connectionContext.ServiceLocator = serviceLocator;

			this.controllerFactory = controllerFactory;
			this.tcpClient = tcpClient;
			this.stream = this.tcpClient.GetStream();
			this.reader = new StreamReader(this.stream);
			this.writer = new StreamWriter(this.stream) { AutoFlush = true };

			this.serializer = new JavaScriptSerializer();

			this.pendingClientRequests = new Dictionary<Guid, TaskCompletionSource<string>>();
			this.pendingHandledRequests = new Dictionary<Guid, CancellationTokenSource>();
		}

		public IRequestManager RequestManager => this;

		public void Dispose()
		{
			this.CancelAllPendingRequests();
			this.writer.Dispose();
			this.reader.Dispose();
			this.stream.Dispose();
			this.tcpClient.Dispose();
		}

		public Task WaitForSessionToEnd()
		{
			return this.messageLoop;
		}

		public void StartMessageLoop()
		{
			this.messageLoop = this.HandleMessageLoopAsync();
		}

		public async Task<string> SendRequest(string path, string method, object requestBody = null, CancellationToken cancellationToken = default)
		{
			Guid messageId = Guid.NewGuid();

			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			lock (this.pendingClientRequests)
			{
				this.pendingClientRequests.Add(messageId, tcs);
			}

			string body = requestBody != null ? this.serializer.Serialize(requestBody) : string.Empty;

			Message message = new Message { Path = path, Method = method, Body = body };
			message.MessageId = messageId;

			cancellationToken.Register(async () =>
			{
				await this.SendMessage(new Message { Path = Paths.Cancellation, Method = Methods.Cancellation.Cancel }, messageId).ConfigureAwait(false);
			});

			await this.SendMessage(message, messageId).ConfigureAwait(false);

			return await tcs.Task.ConfigureAwait(false);
		}

		private async Task HandleMessageLoopAsync()
		{
			try
			{
				while (true) // Keep reading as long as the connection is open
				{
					string jsonRequest = await this.reader.ReadLineAsync();
					if (string.IsNullOrEmpty(jsonRequest))
					{
						break; // If the client closes connection, exit loop
					}

					Message request = this.serializer.Deserialize<Message>(jsonRequest);
					Console.WriteLine($"Received: Id:{request.MessageId} Method={request.Method}, Path={request.Path}, Body={request.Body}");

					switch (request.Path)
					{
						case Paths.Response:
							this.HandleRequestReponse(request);
							break;
						case Paths.Cancellation:
							this.HandleRequestCancellation(request);
							break;
						default:
							this.HandleIncommingRequest(request);
							break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Exception: {e.Message}");
			}
			finally
			{
				Console.WriteLine("Closing connection.");
				this.CancelAllPendingRequests();
				this.tcpClient.Close();
				this.connectionContext.Dispose();
			}
		}

		private void CancelAllPendingRequests()
		{
			this.pendingClientRequests.Values.ForEach(tcs => tcs.SetException(new ObjectDisposedException("Disconnected")));
			this.pendingClientRequests.Clear();
		}

		private void HandleRequestReponse(Message response)
		{
			TaskCompletionSource<string> tcs;
			lock (this.pendingClientRequests)
			{
				// Find request inside pending requests
				if (!this.pendingClientRequests.TryGetValue(response.MessageId, out tcs))
				{
					return;
				}
				this.pendingClientRequests.Remove(response.MessageId);
			}

			if (response.Method == Methods.Response.Success)
			{
				tcs.SetResult(response.Body);
			}
			else
			{
				FailureBody failureBody = this.serializer.Deserialize<FailureBody>(response.Body);
				Exception exception = HttpStatusExceptionMapper.GetExceptionForStatusCode(failureBody.HttpStatusCode, failureBody.ErrorMessage);
				tcs.SetException(exception);
			}
		}

		private void HandleRequestCancellation(Message request)
		{
			CancellationTokenSource cancellationTokenSource;
			lock (this.pendingHandledRequests)
			{
				if (!this.pendingHandledRequests.TryGetValue(request.MessageId, out cancellationTokenSource))
				{
					return;
				}
			}

			cancellationTokenSource.Cancel();
		}


		private async void HandleIncommingRequest(Message request)
		{
			// We want to to handle the request logic asynchornously without blocking the thread, but we do not want to wait for it
			// so we can continue to perform the next read on the TCP connection. So this is a void method which is not awaited by the caller.
			// Never do async work inside an async void method. So move the work into the HandleIncommingRequestAsync and await it here.
			// See: https://hackernoon.com/how-to-tame-the-async-void-in-c
			await this.HandleIncommingRequestAsync(request);
		}

		private async Task HandleIncommingRequestAsync(Message request)
		{
			Response response;
			IController controller = this.controllerFactory.CreateController(this.connectionContext, request.Path);

			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			lock (this.pendingHandledRequests)
			{
				this.pendingHandledRequests.Add(request.MessageId, cancellationTokenSource);
			}

			try
			{
				response = await controller.ProcessRequestAsync(request, cancellationTokenSource.Token);
			}
			catch (UnauthorizedAccessException)
			{
				response = Response.Unauthorized;
			}
			catch (OperationCanceledException)
			{
				response = Response.Error("Request was cancelled.", HttpStatusCode.RequestTimeout);
			}
			catch (ObjectDisposedException)
			{
				response = Response.Error("Resource is gone.", HttpStatusCode.Gone);
			}
			catch (Exception ex)
			{
				response = Response.Error("Error in request processing.", HttpStatusCode.InternalServerError);
				Console.WriteLine(ex.Message);
			}
			finally
			{
				lock (this.pendingHandledRequests)
				{
					this.pendingHandledRequests.Remove(request.MessageId);
				}
			}

			// Respond
			Message responseMessage = response.AsMessage();

			try
			{
				await this.SendMessage(responseMessage, request.MessageId);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to send response: {ex.Message}");
			}
		}

		private async Task SendMessage(Message message, Guid messageId)
		{
			message.MessageId = messageId;

			Console.WriteLine($"Going to send: ID: {message.MessageId} Method={message.Method}, Path={message.Path}, Body={message.Body}");

			string messageString = this.serializer.Serialize(message);

			await this.semaphore.WaitAsync();
			try
			{
				Console.WriteLine($"Sending: {messageString}");
				await this.writer.WriteLineAsync(messageString);
			}
			finally
			{
				this.semaphore.Release();
			}
		}
	}
}
