using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dobble.Client.Forms.Services
{
	public class GameInviteEventArgs : EventArgs
	{
		public GameInviteEventArgs(string opponentUserName, CancellationToken cancellationToken)
		{
			this.OpponentUserName = opponentUserName;
			this.CancellationToken = cancellationToken;
			this.GameInviteResponse = new TaskCompletionSource<bool>();
		}

		public string OpponentUserName { get; }

		public CancellationToken CancellationToken { get; }

		public TaskCompletionSource<bool> GameInviteResponse { get; }
	}
}