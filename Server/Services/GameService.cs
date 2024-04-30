using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	internal class GameService : IGameService
	{
		private readonly IUserService userService;
		// A single lock is use here for simplicity. In a real-world scenario, a lock should be used for each game but its harder to implement correctly.
		// Its not worth the effort because we don't assume many concurrent game creations.
		private readonly object syncLock = new object();

		public GameService(IUserService userService)
		{
			this.userService = userService;
		}

		public async Task<Result<Guid>> Invite(ServerConnectionContext currentUserConnectionContext, string opponentUserName, CancellationToken cancellationToken)
		{
			ServerConnectionContext opponentUserConnectionContext;
			if (currentUserConnectionContext.User.UserName == opponentUserName)
			{
				return Result<Guid>.FailureResult("You can't invite yourself to a game");
			}

			lock (this.syncLock)
			{
				Result<ServerConnectionContext> opponentConnectionContextResult = this.GetOpponentUserConnectionContext(currentUserConnectionContext, opponentUserName);
				if (!opponentConnectionContextResult.Success)
				{
					return Result<Guid>.FailureResult(opponentConnectionContextResult.ErrorMessage);
				}
				opponentUserConnectionContext = opponentConnectionContextResult.Value;
			}

			GameInviteUserResponse response = await opponentUserConnectionContext.RequestManager.SendClientGameInvite(currentUserConnectionContext.User.UserName, cancellationToken);

			if (!response.Accepted)
			{
				return Result<Guid>.FailureResult($"User {opponentUserName} declined the game");
			}

			// Create game
			Game game = new Game(currentUserConnectionContext, opponentUserConnectionContext, this.syncLock);

			lock (this.syncLock)
			{
				Result<ServerConnectionContext> opponentConnectionContextResult = this.GetOpponentUserConnectionContext(currentUserConnectionContext, opponentUserName);
				if (!opponentConnectionContextResult.Success)
				{
					return Result<Guid>.FailureResult(opponentConnectionContextResult.ErrorMessage);
				}
				opponentUserConnectionContext = opponentConnectionContextResult.Value;

				currentUserConnectionContext.Game = game;
				opponentUserConnectionContext.Game = game;
			}

			await game.Start();
			return Result<Guid>.SuccessResult(game.Id);
		}

		private Result<ServerConnectionContext> GetOpponentUserConnectionContext(ServerConnectionContext currentUserConnectionContext, string opponentUserName)
		{
			ServerConnectionContext opponentUserConnectionContext;
			// Check if current user is already in a game
			if (currentUserConnectionContext.Game != null)
			{
				return Result<ServerConnectionContext>.FailureResult($"User {currentUserConnectionContext.User.UserName} is already in a game");
			}

			// Check if opponent is online
			if (!this.userService.TryGetSignedinUserConnectionContext(opponentUserName, out opponentUserConnectionContext))
			{
				return Result<ServerConnectionContext>.FailureResult($"User {opponentUserName} is not online");
			}

			// Check if opponent is already in a game
			if (opponentUserConnectionContext.Game != null)
			{
				return Result<ServerConnectionContext>.FailureResult($"User {opponentUserName} is already in a game");
			}
			return Result<ServerConnectionContext>.SuccessResult(opponentUserConnectionContext);
		}

		public Task<bool> TurnSelection(ServerConnectionContext currentUserConnectionContext, int[] selection)
		{
			if (currentUserConnectionContext.Game == null)
			{
				throw new InvalidOperationException("User is not in a game");
			}

			return currentUserConnectionContext.Game.ProcessUserSelection(currentUserConnectionContext, selection);
		}

		public Task LeaveGame(ServerConnectionContext currentUserConnectionContext)
		{
			if (currentUserConnectionContext.Game == null)
			{
				throw new InvalidOperationException("User is not in a game");
			}

			return currentUserConnectionContext.Game.LeaveGame(currentUserConnectionContext);
		}
	}
}
