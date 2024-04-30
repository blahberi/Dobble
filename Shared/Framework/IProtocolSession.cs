using System;
using System.Threading.Tasks;

namespace Dobble.Shared.Framework
{
	public interface IProtocolSession : IDisposable
	{
		IRequestManager RequestManager { get; }

		Task WaitForSessionToEnd();
	}
}
