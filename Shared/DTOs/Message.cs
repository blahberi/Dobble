using System;

namespace Dobble.Shared.DTOs
{
	/// <summary>
	/// This is the structure of the message that is sent between the client and the server.
	/// It is inspired by the HTTP protocol.
	/// </summary>
	public class Message
	{
		public Guid MessageId { get; set; }
		public string Path { get; set; }
		public string Method { get; set; }
		public string Body { get; set; }
	}
}
