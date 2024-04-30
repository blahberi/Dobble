using System.Net.Sockets;

namespace Dobble.Shared.Framework
{
	public interface IProtocolManager
	{
		IProtocolSession CreateSession(TcpClient tcpClient);
	}
}