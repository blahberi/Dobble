﻿using System;

namespace Dobble.Shared.DTOs.Game
{
	/// <summary>
	/// DTO that contains the information about the game that has ended.
	/// </summary>
	public class GameOver
	{
		public Guid GameId { get; set; }
		public string Winner { get; set; }
		public string Player1 { get; set; }
		public int Score1 { get; set; }
		public string Player2 { get; set; }
		public int Score2 { get; set; }
		public string PreviousTurnWinner { get; set; }
		public int[] PreviousTurnIndices { get; set; }
	}
}
