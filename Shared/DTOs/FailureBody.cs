using System.Net;

namespace Dobble.Shared.DTOs
{
	/// <summary>
	/// This is the structure of the body of a failure message.
	/// When a request fails, the other side will send a message with this body
	/// indicating the error that occured.
	/// </summary>
	public class FailureBody
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
