using System;
using Dobble.Server.DataAccess.Models;
using Dobble.Server.Services;
using Dobble.Shared.Framework;

namespace Dobble.Server
{
	/// <summary>
	/// Server side connection context.
	/// </summary>
	internal class ServerConnectionContext : ConnectionContext
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ServerConnectionContext()
		{
			this.User = null;
			this.Game = null;
		}

		public User User { get; set; }

		public Game Game { get; set; }

		public event Action GameInviteCancelled;

		/// <summary>
		/// Disposes the connection context.
		/// </summary>
		override public void Dispose()
		{
			this.SignoutUser();
		}

		/// <summary>
		/// Cancels the game invite.
		/// </summary>
		public void CancelGameInvite()
		{
			GameInviteCancelled?.Invoke();
		}

		/// <summary>
		/// Signs out the user
		/// </summary>
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
