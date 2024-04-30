using System;
using System.Threading.Tasks;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Interface for a protocol session.
	/// The protocol session manages a session between 2 parties.
	/// </summary>
	public interface IProtocolSession : IDisposable
	{
		IRequestManager RequestManager { get; }

		Task WaitForSessionToEnd();
	}
}
