using System;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// The base for all connection contexts which are application specific
	/// </summary>
	public class ConnectionContext : IDisposable
	{
		/// <summary>
		/// The request manager for the current connection.
		/// The request manager is responsible for sending requests and returning the responses.
		/// </summary>
		public IRequestManager RequestManager { get; internal set; }

		/// <summary>
		/// The service locator which is used to find services that have been registered.
		/// </summary>
		public IServiceLocator ServiceLocator { get; internal set; }

		/// <summary>
		/// Dispose function which is called when the connection context is no longer needed.
		/// </summary>
		public virtual void Dispose()
		{
			// Do nothing by default
			// This method is virtual so that derived classes can override it if they need to do something when the connection context is disposed
		}
	}
}
