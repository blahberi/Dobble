namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Extension methods for IServiceLocator
	/// </summary>
	public static class IServiceLocationExtensions
	{
		/// <summary>
		/// Gets a service from the service locator using returns instead of out
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceLocator"></param>
		/// <returns></returns>
		public static T GetService<T>(this IServiceLocator serviceLocator)
		{
			T service;
			serviceLocator.GetService<T>(out service);
			return service;
		}
	}
}
