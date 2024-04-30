using System.Threading.Tasks;
using Dobble.Client.Forms.Services;

namespace Dobble.Client.Forms
{
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

		public void Accept()
		{
			this.gameUIManager.RemoveUser(this);
			this.GameInviteResponse.SetResult(true);
		}

		public void Reject()
		{
			this.gameUIManager.RemoveUser(this);
			this.GameInviteResponse.SetResult(false);
		}

		public void Cancel()
		{
			this.gameUIManager.RemoveUser(this);
			this.GameInviteResponse.SetCanceled();
		}
	}
}
