using System;

namespace Dobble.Shared.DTOs.Game
{
	public class GameNextTurn
	{
		public Guid GameId { get; set; }
		public string Player1 { get; set; }
		public int Score1 { get; set; }
		public string Player2 { get; set; }
		public int Score2 { get; set; }
		public int[][] Cards { get; set; }
		public string PreviousTurnWinner { get; set; }
		public int[] PreviousTurnIndices { get; set; }
	}
}
