using System.IO;
using System.Net.Sockets;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Allows the creation of protocol sessions between a client and a server
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	internal class ProtocolManager<TConnectionContext> : IProtocolManager where TConnectionContext : ConnectionContext, new()
	{
		private readonly IControllerFactory<TConnectionContext> controllerFactory;
		private readonly IServiceLocator serviceLocator;

		/// <summary>
		/// Constructor for a protocol manager
		/// </summary>
		/// <param name="controllerFactory">Creates controllers for handling request</param>
		/// <param name="serviceLocator">Manages the services which are used for logic</param>
		public ProtocolManager(IControllerFactory<TConnectionContext> controllerFactory, IServiceLocator serviceLocator)
		{
			this.controllerFactory = controllerFactory;
			this.serviceLocator = serviceLocator;
		}

		/// <summary>
		/// Creates a session between a client and a server
		/// </summary>
		/// <param name="tcpClient"></param>
		/// <returns></returns>
		public IProtocolSession CreateSession(ISessionComm sessionComm)
		{
			ProtocolSession<TConnectionContext> protocolSession = new ProtocolSession<TConnectionContext>(this.controllerFactory, this.serviceLocator, sessionComm);

			protocolSession.StartMessageLoop();

			return protocolSession;
		}
	}
}
