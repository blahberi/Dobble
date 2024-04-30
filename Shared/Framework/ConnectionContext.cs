using System;

namespace Dobble.Shared.Framework
{
	// The base for all connection contexts which are application specific
	public class ConnectionContext : IDisposable
	{
		public IRequestManager RequestManager { get; internal set; }

		public IServiceLocator ServiceLocator { get; internal set; }

		public virtual void Dispose()
		{
		}
	}
}
