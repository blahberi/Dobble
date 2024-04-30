namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Builds a protocol manager
	/// It allows registering controllers and services before the protocol manager is built
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	public class ProtocolManagerBuilder<TConnectionContext> : IProtocolManagerBuilder<TConnectionContext> where TConnectionContext : ConnectionContext, new()
	{
		private readonly ServiceLocator serviceLocator;
		private readonly ControllerFactory<TConnectionContext> controllerFactory;

		/// <summary>
		/// Constructor for a protocol manager builder
		/// </summary>
		public ProtocolManagerBuilder()
		{
			this.serviceLocator = new ServiceLocator();
			this.controllerFactory = new ControllerFactory<TConnectionContext>();
		}

		/// <summary>
		/// Builds the protocol manager.
		/// </summary>
		/// <returns>a protocol manager that can be used to create connections.</returns>
		public IProtocolManager Build()
		{
			return new ProtocolManager<TConnectionContext>(this.controllerFactory, this.serviceLocator);
		}

		/// <summary>
		/// Registers a specific controller for the protocol manager
		/// </summary>
		/// <param name="path"></param>
		/// <param name="controllerFactory"></param>
		/// <returns> The protocol manager builder, this allows chaining of the register calls</returns>
		public IProtocolManagerBuilder<TConnectionContext> RegisterController(string path, CreateController<TConnectionContext> controllerFactory)
		{
			this.controllerFactory.RegisterController(path, controllerFactory);
			return this;
		}

		/// <summary>
		/// Registers a service with the service locator
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <param name="instance"></param>
		/// <returns> The protocol manager builder, this allows chaining of the register calls</returns>
		public IProtocolManagerBuilder<TConnectionContext> RegisterService<TService>(TService instance) where TService : class
		{
			this.serviceLocator.RegisterService(instance);
			return this;
		}
	}
}
