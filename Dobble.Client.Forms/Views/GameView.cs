using System;
using System.Windows.Forms;

namespace Dobble.Client.Forms.Views
{
	internal partial class GameView : UserControl
	{
		private readonly GameUIManager gameUIManager;
		private readonly string opponentName;
		private int[][] cards;

		public GameView(GameUIManager gameUIManager)
		{
			this.gameUIManager = gameUIManager;
			this.opponentName = gameUIManager.OpponentName;
			this.cards = gameUIManager.Cards;
			this.gameUIManager.OnGameNextTurn += this.Game_OnGameNextTurn;
			this.gameUIManager.OnGameOver += this.Game_OnGameOver;

			this.InitializeComponent();

			this.YourUserNameLabel.Text = gameUIManager.UserName;
			this.OpponentNameLabel.Text = this.opponentName;
			this.YourScoreLabel.Text = gameUIManager.YourScore.ToString();
			this.OpponentScoreLabel.Text = gameUIManager.OpponentScore.ToString();

			this.Card1.OnSymbolSelected += this.Card_OnSymbolSelected;
			this.Card2.OnSymbolSelected += this.Card_OnSymbolSelected;
			this.Card1.LoadSymbols(this.cards[0]);
			this.Card2.LoadSymbols(this.cards[1]);
		}

		private void Game_OnGameOver()
		{
			this.FinishRound();
			this.LastRoundTimer.Start();
		}

		private void Game_OnGameNextTurn()
		{
			this.cards = this.gameUIManager.Cards;
			this.FinishRound();
			this.NewRoundTimer.Start();
		}

		private void FinishRound()
		{
			this.RevealRound();
			bool youWon = this.gameUIManager.YouWonPreviousTurn;
			if (youWon)
			{
				this.RoundSummaryLabel.ForeColor = GruvboxTheme.Green;
				this.RoundSummaryLabel.Text = $"{this.gameUIManager.UserName} won this round!";
			}
			else
			{
				this.RoundSummaryLabel.ForeColor = GruvboxTheme.Red;
				this.RoundSummaryLabel.Text = $"{this.opponentName} won this round!";
			}
			this.RoundSummaryLabel.Visible = true;

			this.YourScoreLabel.Text = this.gameUIManager.YourScore.ToString();
			this.OpponentScoreLabel.Text = this.gameUIManager.OpponentScore.ToString();
		}

		private void RevealRound()
		{
			if (this.gameUIManager.PreviousTurnIndices == null)
			{
				return;
			}
			this.Card1.IsolateSymbol(this.gameUIManager.PreviousTurnIndices[0]);
			this.Card2.IsolateSymbol(this.gameUIManager.PreviousTurnIndices[1]);

			if (this.gameUIManager.YouWonPreviousTurn)
			{
				this.Card1.MarkAsCorrect(this.gameUIManager.PreviousTurnIndices[0]);
				this.Card2.MarkAsCorrect(this.gameUIManager.PreviousTurnIndices[1]);
			}
		}

		private void Card_OnSymbolSelected()
		{

			if (this.Card1.Selected >= 0 && this.Card2.Selected >= 0)
			{
				if (this.cards[0][this.Card1.Selected] == this.cards[1][this.Card2.Selected])
				{
					this.HandleCorrectPair();
				}
				else
				{
					this.HandleIncorrectPair();
				}
			}
		}

		private void HandleIncorrectPair()
		{
			this.Card1.MarkAsWrong(this.Card1.Selected);
			this.Card2.MarkAsWrong(this.Card2.Selected);
			this.WrongTimer.Start();
		}

		private void HandleCorrectPair()
		{
			this.gameUIManager.SendPair(this.Card1.Selected, this.Card2.Selected);
			this.Card1.ResetSelection();
			this.Card2.ResetSelection();
		}

		private void WrongTimer_Tick(object sender, EventArgs e)
		{
			this.WrongTimer.Stop();
			this.Card1.ResetSelection();
			this.Card2.ResetSelection();
		}

		private void NewRoundTimer_Tick(object sender, EventArgs e)
		{
			this.NewRoundTimer.Stop();

			this.RoundSummaryLabel.Visible = false;

			this.Card1.UnmarkAll();
			this.Card2.UnmarkAll();

			this.Card1.LoadSymbols(this.cards[0]);
			this.Card2.LoadSymbols(this.cards[1]);

			this.Card1.ResetIsolation();
			this.Card2.ResetIsolation();
		}

		private void LastRoundTimer_Tick(object sender, EventArgs e)
		{
			this.LastRoundTimer.Stop();
			this.gameUIManager.ClientGameFinished();
		}

		private void LeaveGameButton_Click(object sender, EventArgs e)
		{
			this.gameUIManager.ClientLeaveGame();
		}
	}
}
