using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Dobble.Shared.Framework;

namespace Dobble.Server
{
	internal class TcpServer
	{
		private readonly TcpListener listener;
		private readonly IProtocolManager protocolManager;

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
