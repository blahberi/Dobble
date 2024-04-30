namespace Dobble.Shared.Framework
{
	/// <summary>
	/// A delegate to create a controller.
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	/// <param name="connectionContext"></param>
	/// <returns></returns>
	public delegate IController CreateController<TConnectionContext>(TConnectionContext connectionContext)
		where TConnectionContext : ConnectionContext;

	/// <summary>
	/// Builds a protocol manager.
	/// Allows registering controllers and services before the protocol manager is built.
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	public interface IProtocolManagerBuilder<TConnectionContext> where TConnectionContext : ConnectionContext
	{
		/// <summary>
		/// Register a controller for a specific path.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="createController"></param>
		/// <returns></returns>
		IProtocolManagerBuilder<TConnectionContext> RegisterController(string path, CreateController<TConnectionContext> createController);

		/// <summary>
		/// Register a service with the service locator.
		/// This will allow us to locate that service later.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <param name="instance"></param>
		/// <returns></returns>
		IProtocolManagerBuilder<TConnectionContext> RegisterService<TService>(TService instance) where TService : class;

		IProtocolManager Build();
	}
}
