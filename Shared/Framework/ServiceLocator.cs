using System;
using System.Collections.Generic;

namespace Dobble.Shared.Framework
{
	internal class ServiceLocator : IServiceLocator
	{
		private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

		public void RegisterService<T>(T instance) where T : class
		{
			this.services[typeof(T)] = instance;
		}

		public void GetService<T>(out T instance)
		{
			instance = (T)this.services[typeof(T)];
		}
	}
}
