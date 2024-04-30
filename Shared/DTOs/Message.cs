using System;

namespace Dobble.Shared.DTOs
{
	public class Message
	{
		public Guid MessageId { get; set; }
		public string Path { get; set; }
		public string Method { get; set; }
		public string Body { get; set; }
	}
}
