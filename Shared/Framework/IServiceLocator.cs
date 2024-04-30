namespace Dobble.Shared.Framework
{
	public interface IServiceLocator
	{
		void GetService<T>(out T service);
	}
}
