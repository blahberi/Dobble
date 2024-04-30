using System.Threading.Tasks;
using Dobble.Server.Controllers;
using Dobble.Server.Services;
using Dobble.Shared.Framework;

namespace Dobble.Server
{
	class Program
	{
		static async Task Main(string[] args)
		{
			IProtocolManager protocolManager = ProtocolHost.CreateDefaultBuilder<ServerConnectionContext>() // Get the default protocol manager builder
				.RegisterServices() // Register the services
				.RegisterControllers() // Register the controllers
				.Build(); // Build the protocol manager

			TcpServer tcpServer = new TcpServer(protocolManager, 3005); // Create the TCP server

			await tcpServer.Start(); // Start the server
		}
	}
}
