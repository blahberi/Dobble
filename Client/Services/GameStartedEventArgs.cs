using System;

namespace Dobble.Client.Services
{
    public class GameStartedEventArgs
    {
        public GameStartedEventArgs(Guid gameId, string opponentName, int[][] cards)
        {
            this.GameId = gameId;
            this.OpponentName = opponentName;
            this.Cards = cards;
        }

        public Guid GameId { get; }
        public string OpponentName { get; }
        public int[][] Cards { get; }
    }
}