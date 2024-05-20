using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;

namespace Dobble.Shared.Framework
{
	public class AesSessionStream : ISessionStream
	{
		private TcpClient tcpClient;
		private AesStream aesStream;
		private Aes aes;

		public StreamReader Reader { get; private set; }
		public StreamWriter Writer { get; private set; }

		public AesSessionStream(TcpClient tcpClient, byte[] key, byte[] iv)
		{
			this.tcpClient = tcpClient;
			NetworkStream networkStream = tcpClient.GetStream();
			
			this.aes = Aes.Create();
			this.aes.Key = key;
			this.aes.IV = iv;

			this.aesStream = new AesStream(networkStream, this.aes.CreateEncryptor(), this.aes.CreateDecryptor());
			this.Reader = new StreamReader(this.aesStream);
			this.Writer = new StreamWriter(this.aesStream) { AutoFlush = true };
		}

		public void Dispose()
		{
			this.Reader?.Dispose();
			this.Writer?.Dispose();
			this.aesStream?.Dispose();
			this.aes?.Dispose();
			this.tcpClient?.Close();
		}
	}

	internal class AesStream : Stream
	{
		private Stream baseStream;
		private ICryptoTransform encryptor;
		private ICryptoTransform decryptor;

		public AesStream(Stream baseStream, ICryptoTransform encryptor, ICryptoTransform decryptor)
		{
			this.baseStream = baseStream;
			this.encryptor = encryptor;
			this.decryptor = decryptor;
		}

		public override bool CanRead => this.baseStream.CanRead;
		public override bool CanSeek => false;
		public override bool CanWrite => this.baseStream.CanWrite;
		public override long Length => throw new NotSupportedException();
		public override long Position
		{
			get => throw new NotSupportedException();
			set => throw new NotSupportedException();
		}

		public override void Flush() => this.baseStream.Flush();

		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] tempBuffer = new byte[count];
			int bytesRead = this.baseStream.Read(tempBuffer, 0, count);
			byte[] decryptedBytes = this.decryptor.TransformFinalBlock(tempBuffer, 0, bytesRead);
			Array.Copy(decryptedBytes, 0, buffer, offset, decryptedBytes.Length);
			return decryptedBytes.Length;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			byte[] encryptedBytes = this.encryptor.TransformFinalBlock(buffer, offset, count);
			this.baseStream.Write(encryptedBytes, 0, encryptedBytes.Length);
		}

		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			byte[] tempBuffer = new byte[count];
			int bytesRead = await this.baseStream.ReadAsync(tempBuffer, 0, count, cancellationToken);
			byte[] decryptedBytes = this.decryptor.TransformFinalBlock(tempBuffer, 0, bytesRead);
			Array.Copy(decryptedBytes, 0, buffer, offset, decryptedBytes.Length);
			return decryptedBytes.Length;
		}

		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			byte[] encryptedBytes = this.encryptor.TransformFinalBlock(buffer, offset, count);
			await this.baseStream.WriteAsync(encryptedBytes, 0, encryptedBytes.Length, cancellationToken);
		}

		public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
		public override void SetLength(long value) => throw new NotSupportedException();

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.encryptor?.Dispose();
				this.decryptor?.Dispose();
				this.baseStream?.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
