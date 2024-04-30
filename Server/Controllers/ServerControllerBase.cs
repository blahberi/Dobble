using System;
using Dobble.Shared.Framework;

namespace Dobble.Server.Controllers
{
	internal abstract class ServerControllerBase : ControllerBase<ServerConnectionContext>
	{
		public ServerControllerBase(ServerConnectionContext connectionContext) : base(connectionContext)
		{
		}

		protected bool IsAuthorized => this.ConnectionContext.User != null;

		public void Authorize()
		{
			if (!this.IsAuthorized)
			{
				throw new UnauthorizedAccessException();
			}
		}
	}
}
