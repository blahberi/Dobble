using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Dobble.Shared.Framework
{
	public static class IRequestManagerExtensions
	{
		// Handles a request with a JSON body response
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
