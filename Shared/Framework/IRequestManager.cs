using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dobble.Shared.Framework
{
	public interface IRequestManager
	{
		Task<string> SendRequest(string path, string method, object requestBody = null, CancellationToken cancellationToken = default);
	}
}
