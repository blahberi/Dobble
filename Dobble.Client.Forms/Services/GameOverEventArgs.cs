using System;

namespace Dobble.Client.Forms.Services
{
	public class GameOverEventArgs
	{
		public GameOverEventArgs(Guid gameId, bool youWon, int yourScore, int opponentScore, bool youWonPreviousTurn, int[] previousTurnIndices)
		{
			this.GameId = gameId;
			this.YouWon = youWon;
			this.YourScore = yourScore;
			this.OpponentScore = opponentScore;
			this.YouWonPreviousTurn = youWonPreviousTurn;
			this.PreviousTurnIndices = previousTurnIndices;
		}

		public Guid GameId { get; }
		public bool YouWon { get; }
		public int YourScore { get; }
		public int OpponentScore { get; }
		public bool YouWonPreviousTurn { get; }
		public int[] PreviousTurnIndices { get; }
	}
}