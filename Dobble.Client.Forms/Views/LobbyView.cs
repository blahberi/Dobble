using System;
using System.Threading;
using System.Windows.Forms;

namespace Dobble.Client.Forms.Views
{
	internal partial class LobbyView : UserControl
	{
		private readonly GameUIManager gameUIManager;
		private bool isInviting;
		private CancellationTokenSource inviteCancellationTokenSource;

		public LobbyView(GameUIManager gameUIManager)
		{
			this.gameUIManager = gameUIManager;
			this.gameUIManager.OnGameInvite += this.GameUIManager_OnGameInvite;
			this.InitializeComponent();

			this.LoadInvitingUsers();
			this.UsernameLabel.Text = this.gameUIManager.UserName;
		}

		private void GameUIManager_OnGameInvite()
		{
			this.InviteList.Items.Clear();
			this.LoadInvitingUsers();
		}

		private void LoadInvitingUsers()
		{
			foreach (InvitingUser invitingUser in this.gameUIManager.InvitingUsers)
			{
				this.InviteList.Items.Add(invitingUser);
			}
			this.UpdateButtons();
		}

		private async void InviteButton_Click(object sender, EventArgs e)
		{
			if (this.isInviting == true)
			{
				this.inviteCancellationTokenSource.Cancel();
				return;
			}

			this.isInviting = true;
			this.UpdateButtons();

			this.inviteCancellationTokenSource = new CancellationTokenSource();

			if (!await this.gameUIManager.TryInvite(this.InviteText.Text, this.inviteCancellationTokenSource.Token))
			{
				this.isInviting = false;
			}
			this.UpdateButtons();
		}

		private void AcceptButton_Click(object sender, EventArgs e)
		{
			// Get the selected user
			InvitingUser invitingUser = (InvitingUser)this.InviteList.SelectedItem;
			invitingUser.Accept();
			this.UpdateButtons();
		}

		private void RejectButton_Click(object sender, EventArgs e)
		{
			// Get the selected user
			InvitingUser invitingUser = (InvitingUser)this.InviteList.SelectedItem;
			invitingUser.Reject();
			this.UpdateButtons();
		}

		private void InviteList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UpdateButtons();
		}

		private void UpdateButtons()
		{
			this.InviteButton.Text = this.isInviting ? "Cancel" : "Invite";
			this.InviteText.Enabled = !this.isInviting;

			bool inviteSelected = this.InviteList.SelectedItem != null;
			this.AcceptButton.Enabled = inviteSelected && !this.isInviting;
			this.RejectButton.Enabled = inviteSelected && !this.isInviting;
		}

		private async void SignOutButton_Click(object sender, EventArgs e)
		{
			await this.gameUIManager.Signout();
		}
	}
}
