namespace Dobble.Shared.Framework
{
	public delegate IController CreateController<TConnectionContext>(TConnectionContext connectionContext)
		where TConnectionContext : ConnectionContext;

	public interface IProtocolManagerBuilder<TConnectionContext> where TConnectionContext : ConnectionContext
	{
		IProtocolManagerBuilder<TConnectionContext> RegisterController(string path, CreateController<TConnectionContext> createController);

		IProtocolManagerBuilder<TConnectionContext> RegisterService<TService>(TService instance) where TService : class;

		IProtocolManager Build();
	}
}
