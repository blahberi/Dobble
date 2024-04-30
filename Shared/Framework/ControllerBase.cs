using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	public abstract class ControllerBase<TConnectionContext> : IController where TConnectionContext : ConnectionContext
	{
		protected ControllerBase(TConnectionContext connectionContext)
		{
			this.ConnectionContext = connectionContext;
		}

		protected TConnectionContext ConnectionContext { get; }

		public abstract Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken);

		protected void GetService<T>(out T service)
		{
			this.ConnectionContext.ServiceLocator.GetService(out service);
		}

		protected T GetService<T>()
		{
			T service;
			this.ConnectionContext.ServiceLocator.GetService(out service);
			return service;
		}

		protected T GetRequestBody<T>(Message message)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Deserialize<T>(message.Body);
		}
	}
}
