using Dobble.Client.Services;
using Dobble.Shared.DTOs;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Dobble.Client.Controllers
{
    internal class GameController : ControllerBase<ConnectionContext>
    {
        public GameController(ConnectionContext connectionContext) : base(connectionContext)
        {
        }

        public async override Task<Response> ProcessRequestAsync(Message message)
        {
            switch (message.Method)
            {
                case Methods.GameClient.Invite:
                    return await this.Invite(message);
                case Methods.GameClient.NextTurn:
                    return await this.NextTurn(message);
                case Methods.GameClient.GameOver:
                    return await this.GameOver(message);
                default:
                    return Response.Error("Method not allowed", HttpStatusCode.MethodNotAllowed);
            }
        }

        private async Task<Response> Invite(Message message)
        {
            GameInvite invite = this.GetRequestBody<GameInvite>(message);

            bool accepted = await this.GetService<IGameService>().GameInviteRequested(invite.OpponentUserName);

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
                gameOver.Score2);

            return Response.OK().AsTask();
        }
    }
}
