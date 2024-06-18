using System;
using System.IO;
using System.Threading.Tasks;

namespace Dobble.Shared.Framework
{
	public interface ISessionComm : IDisposable
	{
		Task WriteMessageAsync(byte[] bytes);
		Task<byte[]> ReadMessageAsync();
	}
}
