using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	internal interface IGameService
	{
		Task<Result<Guid>> Invite(ServerConnectionContext currentUserConnectionContext, string opponentUserName, CancellationToken cancellationToken);
		Task<bool> TurnSelection(ServerConnectionContext currentUserConnectionContext, int[] selection);
		Task LeaveGame(ServerConnectionContext currentUserConnectionContext);
	}
}
