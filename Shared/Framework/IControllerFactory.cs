namespace Dobble.Shared.Framework
{
	public interface IControllerFactory<TConnectionContext> where TConnectionContext : ConnectionContext
	{
		IController CreateController(TConnectionContext connectionContext, string path);
	}
}
