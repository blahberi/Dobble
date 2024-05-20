using System;
using System.IO;

namespace Dobble.Shared.Framework
{
	public interface ISessionStream : IDisposable
	{
		StreamReader Reader { get; }
		StreamWriter Writer { get; }
	}

}
