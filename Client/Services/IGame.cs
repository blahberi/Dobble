using Dobble.Shared.Framework;
using System;
using System.Threading.Tasks;

namespace Dobble.Client.Services
{
    internal interface IGame
    {
        event EventHandler<GameInviteEventArgs> OnGameInvite;
        event EventHandler<GameStartedEventArgs> OnGameStarted;
        event EventHandler<GameNextTurnEventArgs> OnGameNextTurn;
        event EventHandler<GameOverEventArgs> OnGameOver;

        void SessionStarted(IProtocolSession protocolSession);

        Task<Result> RequestGameInvite(string opponentUserName);

        Task UpdateTurnSelection(int card1, int card2);
    }
}
