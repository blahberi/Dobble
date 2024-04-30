using System;

namespace Dobble.Client.Forms.Services
{
	public class GameNextTurnEventArgs
	{
		public GameNextTurnEventArgs(Guid gameId, int yourScore, int opponentScore, int[][] cards, bool youWonPreviousTurn, int[] previousTurnIndices)
		{
			this.YourScore = yourScore;
			this.OpponentScore = opponentScore;
			this.Cards = cards;
			this.YouWonPreviousTurn = youWonPreviousTurn;
			this.PreviousTurnIndices = previousTurnIndices;
		}

		public Guid GameId { get; }
		public int YourScore { get; }
		public int OpponentScore { get; }
		public int[][] Cards { get; }
		public bool YouWonPreviousTurn { get; }
		public int[] PreviousTurnIndices { get; }
	}
}