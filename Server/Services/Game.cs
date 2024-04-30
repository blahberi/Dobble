using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dobble.Shared;

namespace Dobble.Server.Services
{
	internal class Game
	{
		private readonly Queue<int[]> deck;
		private readonly object syncLock;
		private int[][] currentCards;
		private int player1Score;
		private int player2Score;

		public Game(ServerConnectionContext player1, ServerConnectionContext player2, object syncLock)
		{
			this.Player1 = player1;
			this.Player2 = player2;
			this.syncLock = syncLock;

			// Shuffle deck
			IEnumerable<int[]> shuffeledDeck =
				GenerateDobbleDeck()
				.Shuffle()
				.Select(x => x.Shuffle().ToArray());

			this.deck = new Queue<int[]>(shuffeledDeck);

			this.currentCards = new int[2][];

			this.player1Score = 0;
			this.player2Score = 0;

			this.Id = Guid.NewGuid();
		}

		public ServerConnectionContext Player1 { get; }
		public ServerConnectionContext Player2 { get; }

		public int Player1Score => this.player1Score;
		public int Player2Score => this.player2Score;

		public Guid Id { get; }

		public bool IsPlayerInGame(ServerConnectionContext user)
		{
			return this.Player1 == user || this.Player2 == user;
		}

		public async Task Start()
		{
			this.TakeCards();

			await Task.WhenAll(this.Player1.SendGameNextTurn(
				this.Id,
				this.Player1.User.UserName,
				this.Player1Score,
				this.Player2.User.UserName,
				this.Player2Score,
				this.currentCards,
				null,
				null),
			this.Player2.SendGameNextTurn(
				this.Id,
				this.Player1.User.UserName,
				this.Player1Score,
				this.Player2.User.UserName,
				this.Player2Score,
				this.currentCards,
				null,
				null));
		}

		public async Task<bool> ProcessUserSelection(ServerConnectionContext user, int[] selection)
		{
			// Check if selection is correct
			ServerConnectionContext opponent = this.GetOpponent(user);

			if (this.currentCards[0][selection[0]] != this.currentCards[1][selection[1]])
			{
				return false;
			}

			// Selection is correct. Update score
			if (user == this.Player1)
			{
				this.player1Score++;
			}
			else
			{
				this.player2Score++;
			}

			string lastRoundWinner = user.User.UserName;

			if (this.TakeCards())
			{
				await Task.WhenAll(
					this.Player1.SendGameNextTurn(
						this.Id,
						this.Player1.User.UserName,
						this.Player1Score,
						this.Player2.User.UserName,
						this.Player2Score,
						this.currentCards,
						lastRoundWinner,
						selection),
					this.Player2.SendGameNextTurn(
						this.Id,
						this.Player1.User.UserName,
						this.Player1Score,
						this.Player2.User.UserName,
						this.Player2Score,
						this.currentCards,
						lastRoundWinner,
						selection));
			}
			else
			{
				string winner = this.player1Score > this.player2Score ? this.Player1.User.UserName : this.Player2.User.UserName;
				this.ExitGame();
				await Task.WhenAll(
					this.Player1.SendGameOver(
						this.Id,
						winner,
						this.Player1.User.UserName,
						this.Player1Score,
						this.Player2.User.UserName,
						this.Player2Score,
						lastRoundWinner,
						selection),
					this.Player2.SendGameOver(
						this.Id,
						winner,
						this.Player1.User.UserName,
						this.Player1Score,
						this.Player2.User.UserName,
						this.Player2Score,
						lastRoundWinner,
						selection));

			}
			return true;
		}

		public async Task LeaveGame(ServerConnectionContext user)
		{
			ServerConnectionContext opponent = this.GetOpponent(user);

			this.ExitGame();

			// Send message to opponent
			await opponent.SendGameOver(
				this.Id,
				opponent.User.UserName,
				this.Player1.User.UserName,
				this.Player1Score,
				this.Player2.User.UserName,
				this.Player2Score,
				opponent.User.UserName,
				null);
		}

		private void ExitGame()
		{
			lock (this.syncLock)
			{
				this.Player1.Game = null;
				this.Player2.Game = null;
			}
		}

		public ServerConnectionContext GetOpponent(ServerConnectionContext user)
		{
			if (this.Player1 == user)
			{
				return this.Player2;
			}
			else if (this.Player2 == user)
			{
				return this.Player1;
			}
			else
			{
				throw new InvalidOperationException("User is not in game");
			}
		}

		private bool TakeCards()
		{
			if (this.deck.Count <= 1)
			{
				return false;
			}

			this.currentCards[0] = this.deck.Dequeue();
			this.currentCards[1] = this.deck.Dequeue();

			return true;
		}

		private static List<int>[] GenerateDobbleDeck()
		{
			Tuple<int, int, int>[] points = new Tuple<int, int, int>[GameConfig.ORDER * GameConfig.ORDER + GameConfig.ORDER + 1];
			points[0] = new Tuple<int, int, int>(0, 0, 1);
			int count = 1;
			for (int i = 0; i < GameConfig.ORDER; i++)
			{
				for (int j = 0; j < GameConfig.ORDER; j++)
				{
					points[count++] = new Tuple<int, int, int>(1, i, j);
				}
				points[count++] = new Tuple<int, int, int>(0, 1, i);
			}

			Tuple<int, int, int>[] lines = new Tuple<int, int, int>[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				lines[i] = points[i];
			}

			List<int>[] deck = new List<int>[GameConfig.ORDER * GameConfig.ORDER + GameConfig.ORDER + 1];
			for (int i = 0; i < points.Length; i++)
			{
				for (int j = 0; j < lines.Length; j++)
				{
					Tuple<int, int, int> point = points[i];
					Tuple<int, int, int> line = lines[j];
					if ((point.Item1 * line.Item1 + point.Item2 * line.Item2 + point.Item3 * line.Item3) % GameConfig.ORDER == 0)
					{
						if (deck[i] == null)
						{
							deck[i] = new List<int>();
						}
						deck[i].Add(j);
					}
				}
			}
			return deck;
		}
	}
}
