using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Interface for a controller.
	/// Controllers are responsible for processing requests from a specific path
	/// </summary>
	public interface IController
	{
		/// <summary>
		/// Process the request and return a response as task 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken);
	}
}
