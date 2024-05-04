using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dobble.Shared
{
	public static class EncryptedTcp
	{
		/// <summary>
		/// Sets up an encrypted stream for a tcp client.
		/// </summary>
		/// <param name="client">tcp client to create the stream from</param>
		/// <param name="key">aes key</param>
		/// <param name="iv">aes iv</param>
		/// <returns></returns>
		public static Stream SetupEncryptedStream(TcpClient client, byte[] key, byte[] iv)
		{
			AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider
			{
				Key = key,
				IV = iv
			};
			NetworkStream netStream = client.GetStream();
			ICryptoTransform encryptor = aesProvider.CreateEncryptor();
			ICryptoTransform decryptor = aesProvider.CreateDecryptor();

			return new DuplexCryptoStream(netStream, encryptor, decryptor);
		}
	}

	internal class DuplexCryptoStream : Stream
	{
		private Stream baseStream;
		private ICryptoTransform encryptor;
		private ICryptoTransform decryptor;

		public DuplexCryptoStream(Stream baseStream, ICryptoTransform encryptor, ICryptoTransform decryptor)
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
