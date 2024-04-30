using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Server.Services;
using Dobble.Shared.DTOs;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Server.Controllers
{
	internal class GameController : ServerControllerBase
	{
		public GameController(ServerConnectionContext connectionContext) : base(connectionContext)
		{
		}

		public override async Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken)
		{
			switch (message.Method)
			{
				case Methods.GameServer.Invite:
					return await this.Invite(message, cancellationToken);
				case Methods.GameServer.TurnSelection:
					return await this.TurnSelection(message);
				case Methods.GameServer.Leave:
					return await this.LeaveGame(message);
				default:
					return Response.Error("Method not allowed", HttpStatusCode.MethodNotAllowed);
			}
		}

		private async Task<Response> Invite(Message message, CancellationToken cancellationToken)
		{
			this.Authorize();

			GameInvite invite = this.GetRequestBody<GameInvite>(message);

			Result<Guid> response = await this.GetService<IGameService>().Invite(this.ConnectionContext, invite.OpponentUserName, cancellationToken);
			GameInviteServerResponse gameInviteServerResponse = new GameInviteServerResponse();
			if (response.Success)
			{
				gameInviteServerResponse.GameId = response.Value;
			}
			else
			{
				gameInviteServerResponse.ErrorMessage = response.ErrorMessage;
			}
			return Response.OK(gameInviteServerResponse);

		}

		private async Task<Response> TurnSelection(Message message)
		{
			this.Authorize();

			GameTurnSelection turnSelection = this.GetRequestBody<GameTurnSelection>(message);

			bool isSelectionCorrect = await this.GetService<IGameService>().TurnSelection(this.ConnectionContext, turnSelection.SelectedIndices);

			GameTurnSelectionResponse response = new GameTurnSelectionResponse
			{
				IsSelectionCorrect = isSelectionCorrect
			};

			return Response.OK(response);
		}

		private async Task<Response> LeaveGame(Message message)
		{
			this.Authorize();

			await this.GetService<IGameService>().LeaveGame(this.ConnectionContext);

			return Response.OK();
		}
	}
}
