using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Dobble.Shared.Framework;

namespace Dobble.Server
{
	/// <summary>
	/// The TCP server that listens for incoming connections.
	/// This is the lowest level of the server (in terms of abstraction)
	/// </summary>
	internal class TcpServer
	{
		private readonly TcpListener listener;
		private readonly IProtocolManager protocolManager;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="protocolManager">Protocol Manager which will allow the TCP client to create protcol sessions</param>
		/// <param name="port">The the server is listening on</param>
		public TcpServer(IProtocolManager protocolManager, int port)
		{
			this.listener = new TcpListener(IPAddress.Any, port);
			this.protocolManager = protocolManager;
		}

		public async Task Start()
		{
			this.listener.Start();
			Console.WriteLine("Server started...");

			try
			{
				while (true)
				{
					TcpClient client = await this.listener.AcceptTcpClientAsync();
					Console.WriteLine("Client connected.");
					// Handle the request in a new task
					_ = Task.Run(() => this.HandleClientAsync(client));
				}
			}
			finally
			{
				this.listener.Stop();
			}
		}

		private async Task HandleClientAsync(TcpClient client)
		{
			using (IProtocolSession session = this.protocolManager.CreateSession(client))
			{
				await session.WaitForSessionToEnd();
			}
		}
	}
}
