namespace Dobble.Shared.Framework
{
	public static class IServiceLocationExtensions
	{
		public static T GetService<T>(this IServiceLocator serviceLocator)
		{
			T service;
			serviceLocator.GetService<T>(out service);
			return service;
		}
	}
}
