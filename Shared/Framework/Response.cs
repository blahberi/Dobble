using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	public class Response
	{
		private Response(string responseText, HttpStatusCode httpStatusCode)
		{
			this.ResponseText = responseText;
			this.HttpStatusCode = httpStatusCode;
		}

		public static Response Unauthorized = Error("Unauthorized access", HttpStatusCode.Unauthorized);

		public string ResponseText { get; }

		public HttpStatusCode HttpStatusCode { get; }

		public static Response OK()
		{
			return new Response(null, HttpStatusCode.OK);
		}

		public static Response OK(string responseText)
		{
			return new Response(responseText, HttpStatusCode.OK);
		}

		public static Response OK<T>(T responseValue)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string jsonString = serializer.Serialize(responseValue);

			return new Response(jsonString, HttpStatusCode.OK);
		}

		public static Response Error(string responseText, HttpStatusCode httpStatusCode)
		{
			return new Response(responseText, httpStatusCode);
		}

		public Task<Response> AsTask()
		{
			return Task.FromResult(this);
		}

		public Message AsMessage()
		{
			if (this.HttpStatusCode == HttpStatusCode.OK)
			{
				return new Message { Path = Paths.Response, Method = Methods.Response.Success, Body = this.ResponseText };
			}
			else
			{
				FailureBody body = new FailureBody { HttpStatusCode = this.HttpStatusCode, ErrorMessage = this.ResponseText };
				JavaScriptSerializer serializer = new JavaScriptSerializer();
				string jsonBody = serializer.Serialize(body);

				return new Message { Path = Paths.Response, Method = Methods.Response.Failure, Body = jsonBody };
			}
		}
	}
}
