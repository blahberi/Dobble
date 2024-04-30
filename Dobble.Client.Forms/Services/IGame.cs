using System;
using System.Threading;
using System.Threading.Tasks;
using Dobble.Shared.Framework;

namespace Dobble.Client.Forms.Services
{
	internal interface IGame
	{
		event EventHandler<GameInviteEventArgs> OnGameInvite;
		event EventHandler<GameStartedEventArgs> OnGameStarted;
		event EventHandler<GameNextTurnEventArgs> OnGameNextTurn;
		event EventHandler<GameOverEventArgs> OnGameOver;

		void SessionStarted(IProtocolSession protocolSession);

		Task<Result> RequestGameInvite(string opponentUserName, CancellationToken cancellationToken);

		Task UpdateTurnSelection(int card1, int card2);
	}
}
