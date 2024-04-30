using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// A controller that handles unknown routes
	/// </summary>
	internal class UnknownRouteController : IController
	{
		/// <summary>
		/// Processes the request and returns a response
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellation"></param>
		/// <returns></returns>
		public Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellation)
		{
			return Response.Error($"{message.Path} not found", HttpStatusCode.NotFound).AsTask();
		}
	}
}