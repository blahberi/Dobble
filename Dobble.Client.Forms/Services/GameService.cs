using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Client.Forms.Services
{
	internal class GameService : IGame, IGameService
	{
		private string userName;
		private IProtocolSession protocolSession;
		private Guid gameId;

		public GameService(string userName)
		{
			this.userName = userName;
		}
		public GameService() { }

		public event EventHandler<GameInviteEventArgs> OnGameInvite;
		public event EventHandler<GameNextTurnEventArgs> OnGameNextTurn;
		public event EventHandler<GameOverEventArgs> OnGameOver;
		public event EventHandler<GameStartedEventArgs> OnGameStarted;

		public void SetUserName(string userName)
		{
			this.userName = userName;
		}

		public async Task<Result> RequestGameInvite(string opponentUserName, CancellationToken cancellationToken)
		{
			GameInviteServerResponse response = await this.protocolSession.RequestManager.SendServerGameInvite(opponentUserName, cancellationToken);
			this.gameId = response.GameId;
			return response.GameId != Guid.Empty ? Result.SuccessResult() : Result.FailureResult(response.ErrorMessage);
		}

		public Task UpdateTurnSelection(int card1, int card2)
		{
			return this.protocolSession.RequestManager.SendGameTurnSelection(this.gameId, new int[] { card1, card2 });
		}

		public Task<bool> GameInviteRequested(string opponentUserName, CancellationToken cancellationToken)
		{
			GameInviteEventArgs eventArgs = new GameInviteEventArgs(opponentUserName, cancellationToken);
			this.OnGameInvite?.Invoke(this, eventArgs);
			return eventArgs.GameInviteResponse.Task;
		}

		public void GameNextTurn(
			Guid gameId,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			int[][] cards,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			int yourScore = this.userName == player1Name ? player1Score : player2Score;
			int opponentScore = this.userName == player1Name ? player2Score : player1Score;

			if (previousTurnWinner == null)
			{
				string opponentName = this.userName == player1Name ? player2Name : player1Name;
				GameStartedEventArgs eventArgs = new GameStartedEventArgs(gameId, opponentName, cards);
				this.OnGameStarted?.Invoke(this, eventArgs);
			}
			else
			{
				bool youWonPreviousRound = this.userName == previousTurnWinner;

				GameNextTurnEventArgs eventArgs = new GameNextTurnEventArgs(gameId, yourScore, opponentScore, cards, youWonPreviousRound, previousTurnIndices);
				this.OnGameNextTurn?.Invoke(this, eventArgs);
			}
		}

		public void GameOver(
			Guid gameId,
			string winner,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			bool youWon = this.userName == winner;
			bool youWonPreviousTurn = this.userName == previousTurnWinner;
			int yourScore = this.userName == player1Name ? player1Score : player2Score;
			int opponentScore = this.userName == player1Name ? player2Score : player1Score;
			this.OnGameOver?.Invoke(this, new GameOverEventArgs(gameId, youWon, yourScore, opponentScore, youWonPreviousTurn, previousTurnIndices));
		}

		public void SessionStarted(IProtocolSession protocolSession)
		{
			this.protocolSession = protocolSession;
		}

		public void LeaveGame()
		{
			this.protocolSession.RequestManager.SendLeaveGame(this.gameId);
		}
	}
}
