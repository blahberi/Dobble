using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	internal class UnknownRouteController : IController
	{
		public Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellation)
		{
			return Response.Error($"{message.Path} not found", HttpStatusCode.NotFound).AsTask();
		}
	}
}