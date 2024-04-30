using System;

namespace Dobble.Shared.DTOs.Game
{
	/// <summary>
	/// DTO that contains the indices of the cards that the player selected.
	/// When a player selects the 2 symbols that he thinks are the same, the client will send a message with this body.
	/// </summary>
	public class GameTurnSelection
	{
		public Guid GameId { get; set; }
		public int[] SelectedIndices { get; set; }
	}
}
