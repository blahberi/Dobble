namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Create controllers given the path of the request
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	public interface IControllerFactory<TConnectionContext> where TConnectionContext : ConnectionContext
	{
		/// <summary>
		/// Create a controller for the given path
		/// </summary>
		/// <param name="connectionContext"></param>
		/// <param name="path"></param>
		/// <returns></returns>
		IController CreateController(TConnectionContext connectionContext, string path);
	}
}
