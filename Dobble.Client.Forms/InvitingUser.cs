using System.Threading.Tasks;
using Dobble.Client.Forms.Services;

namespace Dobble.Client.Forms
{
	/// <summary>
	/// Class that represents a user that is inviting the current user to a game.
	/// </summary>
	internal class InvitingUser
	{
		private readonly GameUIManager gameUIManager;
		public InvitingUser(GameUIManager gameUIManager, GameInviteEventArgs e)
		{
			this.UserName = e.OpponentUserName;
			this.GameInviteResponse = e.GameInviteResponse;
			this.gameUIManager = gameUIManager;
		}

		public string UserName { get; }

		public TaskCompletionSource<bool> GameInviteResponse { get; }

		public override string ToString() => this.UserName;

		/// <summary>
		/// Accept the invitation
		/// </summary>
		public void Accept()
		{
			this.gameUIManager.RemoveInvitingUser(this);
			this.GameInviteResponse.SetResult(true);
		}

		/// <summary>
		/// Reject the invitation
		/// </summary>
		public void Reject()
		{
			this.gameUIManager.RemoveInvitingUser(this);
			this.GameInviteResponse.SetResult(false);
		}

		public void Cancel()
		{
			this.gameUIManager.RemoveInvitingUser(this);
			this.GameInviteResponse.SetCanceled();
		}
	}
}
