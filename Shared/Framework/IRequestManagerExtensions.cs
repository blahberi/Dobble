using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// Extension methods for IRequestManager
	/// </summary>
	public static class IRequestManagerExtensions
	{
		/// <summary>
		/// Sends a request and returns the response as a deserialized object
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <param name="requestManager"></param>
		/// <param name="path"></param>
		/// <param name="method"></param>
		/// <param name="requestBody"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static async Task<TResponse> SendRequest<TResponse>(this IRequestManager requestManager, string path, string method, object requestBody = null, CancellationToken cancellationToken = default)
		{
			string responseString = await requestManager.SendRequest(path, method, requestBody, cancellationToken);

			if (responseString == null)
			{
				return default;
			}

			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Deserialize<TResponse>(responseString);
		}
	}
}
