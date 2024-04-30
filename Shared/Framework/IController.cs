using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	public interface IController
	{
		Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken);
	}
}
