using System.Net.Sockets;

namespace Dobble.Shared.Framework
{
	internal class ProtocolManager<TConnectionContext> : IProtocolManager where TConnectionContext : ConnectionContext, new()
	{
		private readonly IControllerFactory<TConnectionContext> controllerFactory;
		private readonly IServiceLocator serviceLocator;

		public ProtocolManager(IControllerFactory<TConnectionContext> controllerFactory, IServiceLocator serviceLocator)
		{
			this.controllerFactory = controllerFactory;
			this.serviceLocator = serviceLocator;
		}

		public IProtocolSession CreateSession(TcpClient tcpClient)
		{
			ProtocolSession<TConnectionContext> protocolSession = new ProtocolSession<TConnectionContext>(this.controllerFactory, this.serviceLocator, tcpClient);

			protocolSession.StartMessageLoop();

			return protocolSession;
		}
	}
}
