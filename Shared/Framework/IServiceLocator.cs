namespace Dobble.Shared.Framework
{
	/// <summary>
	/// The interface for a service locator.
	/// Service locators allow us to locate services that are registered with the service locator
	/// </summary>
	public interface IServiceLocator
	{
		/// <summary>
		///  Find a service by its type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="service"></param>
		void GetService<T>(out T service);
	}
}
