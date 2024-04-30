using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Interface for a request manager.
	/// Request managers are responsible for sending requests to a server.
	/// </summary>
	public interface IRequestManager
	{
		Task<string> SendRequest(string path, string method, object requestBody = null, CancellationToken cancellationToken = default);
	}
}
