﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dobble.Client.Forms.Services
{
	internal interface IGameService
	{
		Task<bool> GameInviteRequested(string opponentUserName, CancellationToken cancellationToken);
		void GameNextTurn(
			Guid gameId,
			string player1Name,
			int player1Score,
			string player2Name,
			int player2Score,
			int[][] cards,
			string previousTurnWinner,
			int[] previousTurnIndices);
		void GameOver(Guid gameId, string winner, string player1, int score1, string player2, int score2, string previousTurnWinner, int[] previousTurnIndices);
	}
}