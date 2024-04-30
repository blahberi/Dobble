using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dobble.Client.Forms.Views
{
	internal partial class GameOverView : UserControl
	{
		private GameUIManager gameUIManager;

		public GameOverView(GameUIManager gameUIManager)
		{
			this.gameUIManager = gameUIManager;
			this.InitializeComponent();
		}

		private void GameOverView_Load(object sender, EventArgs e)
		{
			this.WhoWonLabel.Text = this.gameUIManager.YouWon ? "You won!" : "You lost!";
			this.WhoWonLabel.ForeColor = this.gameUIManager.YouWon ? GruvboxTheme.Green : GruvboxTheme.Red;

			this.YourUserNameLabel.Text = this.gameUIManager.UserName;
			this.OpponentNameLabel.Text = this.gameUIManager.OpponentName;

			this.YourScoreLabel.Text = this.gameUIManager.YourScore.ToString();
			this.OpponentScoreLabel.Text = this.gameUIManager.OpponentScore.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.gameUIManager.ClientReturnToLobby();
		}
	}
}
