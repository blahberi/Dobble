using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;

namespace Dobble.Shared.Framework
{
	public class EncryptedTcpComm : ISessionComm
	{
		private TcpClient tcpClient;
		private NetworkStream networkStream;
		private Aes aes;
		private ICryptoTransform encryptor;
		private ICryptoTransform decryptor;

		public EncryptedTcpComm(TcpClient tcpClient, byte[] key, byte[] iv)
		{
			this.tcpClient = tcpClient;
			this.networkStream = tcpClient.GetStream();

			this.aes = Aes.Create();
			this.aes.Key = key;
			this.aes.IV = iv;

			this.encryptor = this.aes.CreateEncryptor();
			this.decryptor = this.aes.CreateDecryptor();
		}

		public async Task WriteMessageAsync(byte[] bytes)
		{
			byte[] encryptedBytes = this.encryptor.TransformFinalBlock(bytes, 0, bytes.Length); // Encrypt the bytes
			byte[] lengthBytes = BitConverter.GetBytes(encryptedBytes.Length); // Get the length of the encrypted bytes
			await this.networkStream.WriteAsync(lengthBytes, 0, 4); // Write the length of the encrypted bytes
			await this.networkStream.WriteAsync(encryptedBytes, 0, encryptedBytes.Length); // Write the encrypted bytes
		}

		public async Task<byte[]> ReadMessageAsync()
		{
			byte[] lengthBytes = new byte[4];
			await this.networkStream.ReadAsync(lengthBytes, 0, 4); // Read the length of the encrypted bytes
			int length = BitConverter.ToInt32(lengthBytes, 0); // Get the length of the encrypted bytes
			byte[] encryptedBytes = new byte[length];
			await this.networkStream.ReadAsync(encryptedBytes, 0, length); // Read the encrypted bytes
			byte[] decryptedBytes = this.decryptor.TransformFinalBlock(encryptedBytes, 0, length); // Decrypt the bytes
			return decryptedBytes;
		}

		public void Dispose()
		{
			this.aes.Dispose();
			this.networkStream.Close();
			this.tcpClient.Close();
		}
	}
}