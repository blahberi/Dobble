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

		/// <summary>
		/// Checks if a user is signed in.
		/// </summary>
		/// <exception cref="UnauthorizedAccessException"></exception>
		public void Authorize()
		{
			if (!this.IsAuthorized)
			{
				throw new UnauthorizedAccessException();
			}
		}
	}
}
