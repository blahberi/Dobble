using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	/// <summary>
	/// The server-side game service.
	/// This service is responsible for handling all the games and game related operations.
	/// </summary>
	internal class GameService : IGameService
	{
		private readonly IUserService userService;
		private readonly object syncLock = new object(); // Single lock for all games.

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="userService"></param>
		public GameService(IUserService userService)
		{
			this.userService = userService;
		}

		/// <summary>
		/// Invites an opponent to a game.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <param name="opponentUserName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Get the opponent's user connection context.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <param name="opponentUserName"></param>
		/// <returns></returns>
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

		/// <summary>
		/// When a client sends it's symbol selection for the game,
		/// this method is called to check it and return whether it is correct or not.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <param name="selection"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public Task<bool> TurnSelection(ServerConnectionContext currentUserConnectionContext, int[] selection)
		{
			if (currentUserConnectionContext.Game == null)
			{
				throw new InvalidOperationException("User is not in a game");
			}

			return currentUserConnectionContext.Game.ProcessUserSelection(currentUserConnectionContext, selection);
		}

		/// <summary>
		/// This method is called when a client wants to leave a game.
		/// </summary>
		/// <param name="currentUserConnectionContext"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
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
