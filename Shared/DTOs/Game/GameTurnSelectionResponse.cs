namespace Dobble.Shared.DTOs.Game
{
	/// <summary>
	/// DTO of a game turn selection response message.
	/// When a player selects the 2 symbols that he thinks are the same,
	/// the server will send a message with this body indicating if the selection is correct.
	/// </summary>
	public class GameTurnSelectionResponse
	{
		public bool IsSelectionCorrect { get; set; }
	}
}
