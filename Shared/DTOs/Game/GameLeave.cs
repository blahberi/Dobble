using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dobble.Shared.DTOs.Game
{
	/// <summary>
	/// DTO for when a player leaves a game.
	/// </summary>
	internal class GameLeave
	{
		public Guid GameId { get; set; }
	}
}
