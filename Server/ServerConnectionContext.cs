using System;
using Dobble.Server.DataAccess.Models;
using Dobble.Server.Services;
using Dobble.Shared.Framework;

namespace Dobble.Server
{
	internal class ServerConnectionContext : ConnectionContext
	{
		public ServerConnectionContext()
		{
			this.User = null;
			this.Game = null;
		}

		public User User { get; set; }

		public Game Game { get; set; }

		public event Action GameInviteCancelled;

		override public void Dispose()
		{
			this.SignoutUser();
		}

		public void CancelGameInvite()
		{
			GameInviteCancelled?.Invoke();
		}

		public void SignoutUser()
		{
			if (this.Game != null)
			{
				_ = this.Game.LeaveGame(this);
			}

			if (this.User != null)
			{
				this.ServiceLocator.GetService<IUserService>().SignoutUser(this.User);

				this.User = null;
			}
		}
	}
}
