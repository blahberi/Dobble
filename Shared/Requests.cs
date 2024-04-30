using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.DTOs;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.DTOs.Users;
using Dobble.Shared.Framework;

namespace Dobble.Shared
{
	public static class Requests
	{
		public static Task SendUserRegistration(
			this ConnectionContext connectionContext,
			string userName,
			string password,
			string email,
			string firstName,
			string lastName,
			string country,
			string city,
			string gender)
		{
			return connectionContext.RequestManager.SendUserRegistration(
				userName,
				password,
				email,
				firstName,
				lastName,
				country,
				city,
				gender);
		}

		public static Task SendUserSignin(
			this ConnectionContext connectionContext,
			string userName,
			string password)
		{
			return connectionContext.RequestManager.SendUserSignin(userName, password);
		}

		public static Task SendUserSignout(
			this ConnectionContext connectionContext,
			string userName)
		{
			return connectionContext.RequestManager.SendUserSignout(userName);
		}

		public static Task<GameInviteServerResponse> SendServerGameInvite(
			this ConnectionContext connectionContext,
			string opponentUsername,
			CancellationToken cancellationToken)
		{
			return connectionContext.RequestManager.SendServerGameInvite(opponentUsername, cancellationToken);
		}

		public static Task<GameInviteUserResponse> SendClientGameInvite(
			this ConnectionContext connectionContext,
			string opponentUsername,
			CancellationToken cancellationToken)
		{
			return connectionContext.RequestManager.SendClientGameInvite(opponentUsername, cancellationToken);
		}

		public static Task SendGameNextTurn(
			this ConnectionContext connectionContext,
			in Guid gameId,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			int[][] cards,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			return connectionContext.RequestManager.SendGameNextTurn(
				gameId, player1Name, player1Score, player2Name, player2Score, cards, previousTurnWinner, previousTurnIndices);
		}

		public static Task SendGameOver(
			this ConnectionContext connectionContext,
			in Guid gameId,
			string winner,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			return connectionContext.RequestManager.SendGameOver(gameId, winner, player1Name, player1Score, player2Name, player2Score, previousTurnWinner, previousTurnIndices);
		}

		public static Task SendLeaveGame(
			this ConnectionContext connectionContext,
			in Guid gameId)
		{
			return connectionContext.RequestManager.SendLeaveGame(gameId);
		}

		public static Task SendGameTurnSelection(
			this ConnectionContext connectionContext,
			Guid gameId,
			int[] selectedIndices)
		{
			return connectionContext.RequestManager.SendGameTurnSelection(gameId, selectedIndices);
		}


		// Extension methods

		public static Task SendUserRegistration(
			this IRequestManager requestManager,
			string userName,
			string password,
			string email,
			string firstName,
			string lastName,
			string country,
			string city,
			string gender)
		{
			return requestManager.SendRequest(Paths.Users, Methods.Users.Register, new UserRegistration
			{
				Username = userName,
				Password = password,
				Email = email,
				FirstName = firstName,
				LastName = lastName,
				Country = country,
				City = city,
				Gender = gender
			});
		}

		public static Task SendUserSignin(
			this IRequestManager requestManager,
			string userName,
			string password)
		{
			return requestManager.SendRequest(Paths.Users, Methods.Users.Signin, new UserSignin
			{
				Username = userName,
				Password = password
			});
		}

		public static Task SendUserSignout(
			this IRequestManager requestManager,
			string userName)
		{
			return requestManager.SendRequest(Paths.Users, Methods.Users.Signout, new UserName
			{
				Username = userName
			});
		}

		public static Task<GameInviteServerResponse> SendServerGameInvite(
			this IRequestManager requestManager,
			string opponentUsername,
			CancellationToken cancellationToken)
		{
			return requestManager.SendRequest<GameInviteServerResponse>(Paths.Game, Methods.GameServer.Invite, new GameInvite
			{
				OpponentUserName = opponentUsername
			}, cancellationToken);
		}

		public static Task<GameInviteUserResponse> SendClientGameInvite(
			this IRequestManager requestManager,
			string opponentUsername,
			CancellationToken cancellationToken)
		{
			return requestManager.SendRequest<GameInviteUserResponse>(Paths.Game, Methods.GameClient.Invite, new GameInvite
			{
				OpponentUserName = opponentUsername
			}, cancellationToken);
		}

		public static Task SendGameNextTurn(
			this IRequestManager requestManager,
			in Guid gameId,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			int[][] cards,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			return requestManager.SendRequest(Paths.Game, Methods.GameClient.NextTurn, new GameNextTurn
			{
				GameId = gameId,
				Player1 = player1Name,
				Score1 = player1Score,
				Player2 = player2Name,
				Score2 = player2Score,
				Cards = cards,
				PreviousTurnWinner = previousTurnWinner,
				PreviousTurnIndices = previousTurnIndices
			});
		}

		public static Task SendGameOver(
			this IRequestManager requestManager,
			in Guid gameId,
			string winner,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			string previousTurnWinner,
			int[] previousTurnIndices)
		{
			return requestManager.SendRequest(Paths.Game, Methods.GameClient.GameOver, new GameOver
			{
				GameId = gameId,
				Winner = winner,
				Player1 = player1Name,
				Score1 = player1Score,
				Player2 = player2Name,
				Score2 = player2Score,
				PreviousTurnWinner = previousTurnWinner,
				PreviousTurnIndices = previousTurnIndices
			});
		}

		public static Task SendLeaveGame(
			this IRequestManager requestManager,
			in Guid gameId)
		{
			return requestManager.SendRequest(Paths.Game, Methods.GameServer.Leave, new GameLeave
			{
				GameId = gameId
			});
		}

		public static Task SendGameTurnSelection(
			this IRequestManager requestManager,
			Guid gameId,
			int[] selectedIndices)
		{
			return requestManager.SendRequest(Paths.Game, Methods.GameServer.TurnSelection, new GameTurnSelection
			{
				GameId = gameId,
				SelectedIndices = selectedIndices
			});
		}
	}
}
