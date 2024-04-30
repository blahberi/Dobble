using System;

namespace Dobble.Shared.DTOs.Game
{
	public class GameTurnSelection
	{
		public Guid GameId { get; set; }
		public int[] SelectedIndices { get; set; }
	}
}
