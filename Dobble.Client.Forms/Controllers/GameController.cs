using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Client.Forms.Services;
using Dobble.Shared.DTOs;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Client.Forms.Controllers
{
	internal class GameController : ControllerBase<ConnectionContext>
	{
		public GameController(ConnectionContext connectionContext) : base(connectionContext)
		{
		}

		public async override Task<Response> ProcessRequestAsync(Message message, CancellationToken cancellationToken)
		{
			switch (message.Method)
			{
				case Methods.GameClient.Invite:
					return await this.Invite(message, cancellationToken);
				case Methods.GameClient.NextTurn:
					return await this.NextTurn(message);
				case Methods.GameClient.GameOver:
					return await this.GameOver(message);
				default:
					return Response.Error("Method not allowed", HttpStatusCode.MethodNotAllowed);
			}
		}

		private async Task<Response> Invite(Message message, CancellationToken cancellationToken)
		{
			GameInvite invite = this.GetRequestBody<GameInvite>(message);

			bool accepted = await this.GetService<IGameService>().GameInviteRequested(invite.OpponentUserName, cancellationToken);

			return Response.OK(new GameInviteUserResponse { Accepted = accepted });
		}

		private Task<Response> NextTurn(Message message)
		{
			GameNextTurn nextTurn = this.GetRequestBody<GameNextTurn>(message);

			this.GetService<IGameService>().GameNextTurn(
				nextTurn.GameId,
				nextTurn.Player1,
				nextTurn.Score1,
				nextTurn.Player2,
				nextTurn.Score2,
				nextTurn.Cards,
				nextTurn.PreviousTurnWinner,
				nextTurn.PreviousTurnIndices);

			return Response.OK().AsTask();
		}

		private Task<Response> GameOver(Message message)
		{
			GameOver gameOver = this.GetRequestBody<GameOver>(message);

			this.GetService<IGameService>().GameOver(
				gameOver.GameId,
				gameOver.Winner,
				gameOver.Player1,
				gameOver.Score1,
				gameOver.Player2,
				gameOver.Score2,
				gameOver.PreviousTurnWinner,
				gameOver.PreviousTurnIndices);

			return Response.OK().AsTask();
		}
	}
}
