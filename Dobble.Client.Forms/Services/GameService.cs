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

		/// <summary>
		/// Set the user name.
		/// </summary>
		/// <param name="userName"></param>
		public void SetUserName(string userName)
		{
			this.userName = userName;
		}

		/// <summary>
		/// Invite an opponent to a game.
		/// </summary>
		/// <param name="opponentUserName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Result> RequestGameInvite(string opponentUserName, CancellationToken cancellationToken)
		{
			GameInviteServerResponse response = await this.protocolSession.RequestManager.SendServerGameInvite(opponentUserName, cancellationToken);
			this.gameId = response.GameId;
			return response.GameId != Guid.Empty ? Result.SuccessResult() : Result.FailureResult(response.ErrorMessage);
		}

		/// <summary>
		/// Submits the selected symbols by the user to be sent to the server.
		/// </summary>
		/// <param name="card1"></param>
		/// <param name="card2"></param>
		/// <returns></returns>
		public Task SubmitTurnSelection(int card1, int card2)
		{
			return this.protocolSession.RequestManager.SendGameTurnSelection(this.gameId, new int[] { card1, card2 });
		}

		/// <summary>
		/// Receive a game invite from an opponent.
		/// </summary>
		/// <param name="opponentUserName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task<bool> GameInviteRequested(string opponentUserName, CancellationToken cancellationToken)
		{
			GameInviteEventArgs eventArgs = new GameInviteEventArgs(opponentUserName, cancellationToken);
			this.OnGameInvite?.Invoke(this, eventArgs);
			return eventArgs.GameInviteResponse.Task;
		}

		/// <summary>
		/// Receive the next turn from the server.
		/// </summary>
		/// <param name="gameId"></param>
		/// <param name="player1Name"></param>
		/// <param name="player1Score"></param>
		/// <param name="player2Name"></param>
		/// <param name="player2Score"></param>
		/// <param name="cards"></param>
		/// <param name="previousTurnWinner"></param>
		/// <param name="previousTurnIndices"></param>
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

		/// <summary>
		/// Receive the game summary from the server when the game is over.
		/// </summary>
		/// <param name="gameId"></param>
		/// <param name="winner"></param>
		/// <param name="player1Name"></param>
		/// <param name="player1Score"></param>
		/// <param name="player2Name"></param>
		/// <param name="player2Score"></param>
		/// <param name="previousTurnWinner"></param>
		/// <param name="previousTurnIndices"></param>
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

		/// <summary>
		/// Leave the game.
		/// </summary>
		public void LeaveGame()
		{
			this.protocolSession.RequestManager.SendLeaveGame(this.gameId);
		}
	}
}
