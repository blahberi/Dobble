using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dobble.Shared.DTOs;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Base class for all controllers
	/// </summary>
	/// <typeparam name="TConnectionContext"></typeparam>
	public abstract class ControllerBase<TConnectionContext> : IController where TConnectionContext : ConnectionContext
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionContext"></param>
		protected ControllerBase(TConnectionContext connectionContext)
		{
			this.ConnectionContext = connectionContext;
		}

		/// <summary>
		/// The connection context for the current connection this controller is handling
		/// </summary>
		protected TConnectionContext ConnectionContext { get; }

		/// <summary>
		/// Process a request.
		/// This method is responsible for processing the request and returning a response.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public abstract Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken);

		/// <summary>
		/// Get a service from the service locator.
		/// This is useful because sometimes controllers need to use the logic which is implemented in services.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="service"></param>
		protected void GetService<T>(out T service)
		{
			this.ConnectionContext.ServiceLocator.GetService(out service);
		}

		/// <summary>
		/// Get a service from the service locator using return value instead of out parameter.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected T GetService<T>()
		{
			T service;
			this.ConnectionContext.ServiceLocator.GetService(out service);
			return service;
		}

		/// <summary>
		/// Get the request body from the message.
		/// Since the message body is a JSON string, we need to deserialize it to the desired type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <returns></returns>
		protected T GetRequestBody<T>(Message message)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Deserialize<T>(message.Body);
		}
	}
}
