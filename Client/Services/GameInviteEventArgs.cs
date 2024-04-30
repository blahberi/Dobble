using System;
using System.Threading.Tasks;

namespace Dobble.Client.Services
{
    public class GameInviteEventArgs : EventArgs
    {
        public GameInviteEventArgs(string opponentUserName)
        {
            this.OpponentUserName = opponentUserName;
            this.GameInviteResponse = new TaskCompletionSource<bool>();
        }

        public string OpponentUserName { get; }

        public TaskCompletionSource<bool> GameInviteResponse { get; }
    }
}