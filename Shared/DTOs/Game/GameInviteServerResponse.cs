using System;

namespace Dobble.Shared.DTOs.Game
{
	/// <summary>
	/// DTO for the response to a game invite
	/// </summary>
	public class GameInviteServerResponse
	{
		// The ID of the game or an empty Guid if the game could not be started
		public Guid GameId { get; set; }

		// The error message if the game could not be started and GameId is an empty Guid
		public string ErrorMessage { get; set; }
	}
}
