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
			IProtocolManager protocolManager = ProtocolHost.CreateDefaultBuilder<ServerConnectionContext>()
				.RegisterServices()
				.RegisterControllers()
				.Build();

			TcpServer tcpServer = new TcpServer(protocolManager, 3005);

			await tcpServer.Start();
		}
	}
}
