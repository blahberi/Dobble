using Dobble.Shared;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;
using System;
using System.Threading.Tasks;

namespace Dobble.Client.Services
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

        public async Task<Result> RequestGameInvite(string opponentUserName)
        {
            GameInviteServerResponse response = await this.protocolSession.RequestManager.SendServerGameInvite(opponentUserName);
            this.gameId = response.GameId;
            return response.GameId != Guid.Empty ? Result.SuccessResult() : Result.FailureResult(response.ErrorMessage);
        }

        public Task UpdateTurnSelection(int card1, int card2)
        {
            return this.protocolSession.RequestManager.SendGameTurnSelection(this.gameId, new int[] { card1, card2 });
        }

        public Task<bool> GameInviteRequested(string opponentUserName)
        {
            GameInviteEventArgs eventArgs = new GameInviteEventArgs(opponentUserName);
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
                bool youWon = this.userName == previousTurnWinner;

                GameNextTurnEventArgs eventArgs = new GameNextTurnEventArgs(gameId, yourScore, opponentScore, cards, youWon, previousTurnIndices);
                this.OnGameNextTurn?.Invoke(this, eventArgs);
            }
        }

        public void GameOver(Guid gameId, string winner, string player1, int score1, string player2, int score2)
        {
            this.OnGameOver?.Invoke(this, new GameOverEventArgs(gameId, winner, player1, score1, player2, score2));
        }

        public void SessionStarted(IProtocolSession protocolSession)
        {
            this.protocolSession = protocolSession;
        }
    }
}
