using System;

namespace Dobble.Client.Services
{
    public class GameOverEventArgs
    {
        public Guid GameId { get; }

        public GameOverEventArgs(Guid gameId, string winner, string player1, int score1, string player2, int score2)
        {
            this.GameId = gameId;
            this.Winner = winner;
            this.Player1 = player1;
            this.Score1 = score1;
            this.Player2 = player2;
            this.Score2 = score2;
        }

        public string Winner { get; }
        public string Player1 { get; }
        public int Score1 { get; }
        public string Player2 { get; }
        public int Score2 { get; }
    }
}