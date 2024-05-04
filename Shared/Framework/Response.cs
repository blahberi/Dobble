using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// A response that can be either successful or failed
	/// </summary>
	public class Response
	{

		/// <summary>
		/// Constructor for a response
		/// </summary>
		/// <param name="responseText">the response's contents</param>
		/// <param name="httpStatusCode">Response status code</param>
		private Response(string responseText, HttpStatusCode httpStatusCode)
		{
			this.ResponseText = responseText;
			this.HttpStatusCode = httpStatusCode;
		}

		/// <summary>
		///  A response for unauthorized access
		/// </summary>
		public static Response Unauthorized = Error("Unauthorized access", HttpStatusCode.Unauthorized);

		/// <summary>
		/// The response text
		/// </summary>
		public string ResponseText { get; }

		/// <summary>
		/// The HTTP status code of the response
		/// </summary>
		public HttpStatusCode HttpStatusCode { get; }

		/// <summary>
		/// Creates a successful response
		/// </summary>
		/// <returns></returns>
		public static Response OK()
		{
			return new Response(null, HttpStatusCode.OK);
		}

		/// <summary>
		/// Creates a successful response with a response text
		/// </summary>
		/// <param name="responseText">The contents of the response</param>
		/// <returns></returns>
		public static Response OK(string responseText)
		{
			return new Response(responseText, HttpStatusCode.OK);
		}

		/// <summary>
		/// Creates a successful response with a response value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="responseValue">The value of the response</param>
		/// <returns></returns>
		public static Response OK<T>(T responseValue)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string jsonString = serializer.Serialize(responseValue);

			return new Response(jsonString, HttpStatusCode.OK);
		}

		/// <summary>
		/// Creates a failed response with an error
		/// </summary>
		/// <param name="responseText">The contents of the response</param>
		/// <param name="httpStatusCode">The response's status code</param>
		/// <returns></returns>
		public static Response Error(string responseText, HttpStatusCode httpStatusCode)
		{
			return new Response(responseText, httpStatusCode);
		}

		/// <summary>
		/// Creates a task that waits for the response
		/// </summary>
		/// <returns></returns>
		public Task<Response> AsTask()
		{
			return Task.FromResult(this);
		}

		/// <summary>
		/// Converts the response to a message
		/// </summary>
		/// <returns></returns>
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
