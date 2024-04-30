using System.Net;

namespace Dobble.Shared.DTOs
{
	public class FailureBody
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
