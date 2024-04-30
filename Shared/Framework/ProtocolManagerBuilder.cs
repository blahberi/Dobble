namespace Dobble.Shared.Framework
{
	public class ProtocolManagerBuilder<TConnectionContext> : IProtocolManagerBuilder<TConnectionContext> where TConnectionContext : ConnectionContext, new()
	{
		private readonly ServiceLocator serviceLocator;
		private readonly ControllerFactory<TConnectionContext> controllerFactory;

		public ProtocolManagerBuilder()
		{
			this.serviceLocator = new ServiceLocator();
			this.controllerFactory = new ControllerFactory<TConnectionContext>();
		}

		public IProtocolManager Build()
		{
			return new ProtocolManager<TConnectionContext>(this.controllerFactory, this.serviceLocator);
		}

		public IProtocolManagerBuilder<TConnectionContext> RegisterController(string path, CreateController<TConnectionContext> controllerFactory)
		{
			this.controllerFactory.RegisterController(path, controllerFactory);
			return this;
		}

		public IProtocolManagerBuilder<TConnectionContext> RegisterService<TService>(TService instance) where TService : class
		{
			this.serviceLocator.RegisterService(instance);
			return this;
		}
	}
}
