using System;
using System.Collections.Generic;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Manages services for the application
	/// </summary>
	internal class ServiceLocator : IServiceLocator
	{
		private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		/// <summary>
		/// Registers a service with the service locator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">Servive to be registered</param>
		public void RegisterService<T>(T instance) where T : class
		{
			this.services[typeof(T)] = instance;
		}

		/// <summary>
		/// Gets a service from the service locator
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">Service to be found</param>
		public void GetService<T>(out T instance)
		{
			instance = (T)this.services[typeof(T)];
		}
	}
}
