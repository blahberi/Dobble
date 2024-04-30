using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	/// <summary>
	/// Interface for the game service.
	/// </summary>
	internal interface IGameService
	{
		/// <summary>
		/// Invites an opponent to a game.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <param name="opponentUserName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Result<Guid>> Invite(ServerConnectionContext currentUserConnectionContext, string opponentUserName, CancellationToken cancellationToken);

		/// <summary>
		/// When a client sends it's symbol selection for the game,
		/// this method is called to check it and return whether it is correct or not.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <param name="selection"></param>
		/// <returns></returns>
		Task<bool> TurnSelection(ServerConnectionContext currentUserConnectionContext, int[] selection);

		/// <summary>
		/// This method is called when a client wants to leave a game.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <returns></returns>
		Task LeaveGame(ServerConnectionContext currentUserConnectionContext);
	}
}
