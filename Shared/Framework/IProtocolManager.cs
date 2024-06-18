using System.IO;
using System.Net.Sockets;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Interface for a protocol manager.
	/// The protocol manager allows us to create a session between a client and a server
	/// </summary>
	public interface IProtocolManager
	{
		/// <summary>
		/// Creates a session between a client and a server
		/// </summary>
		/// <param name="tcpClient"></param>
		/// <returns></returns>
		IProtocolSession CreateSession(ISessionComm sessionComm);
	}
}