using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dobble.Shared;

namespace Dobble.Server.Services
{
	/// <summary>
	/// Class for a single game.
	/// </summary>
	internal class Game
	{
		private readonly Queue<int[]> deck;
		private readonly object syncLock;
		private int[][] currentCards;
		private int player1Score;
		private int player2Score;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="player1"></param>
		/// <param name="player2"></param>
		/// <param name="syncLock"></param>
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

		/// <summary>
		/// The first player in the game.
		/// </summary>
		public ServerConnectionContext Player1 { get; }

		/// <summary>
		/// The second player in the game.
		/// </summary>
		public ServerConnectionContext Player2 { get; }


		/// <summary>
		/// The score of the first player.
		/// </summary>
		public int Player1Score => this.player1Score;

		/// <summary>
		/// The score of the second player.
		/// </summary>
		public int Player2Score => this.player2Score;

		/// <summary>
		/// The id of the game.
		/// </summary>
		public Guid Id { get; }

		/// <summary>
		/// Checks if the user is in this game.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool IsPlayerInGame(ServerConnectionContext user)
		{
			return this.Player1 == user || this.Player2 == user;
		}

		/// <summary>
		/// Starts the game.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Receives the user selection of symbols and processes it.
		/// </summary>
		/// <param name="user"></param>
		/// <param name="selection"></param>
		/// <returns></returns>
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

		/// <summary>
		/// This method is called when a client wants to leave a game.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Removes the players from the game.
		/// </summary>
		private void ExitGame()
		{
			lock (this.syncLock)
			{
				this.Player1.Game = null;
				this.Player2.Game = null;
			}
		}

		/// <summary>
		/// Gets the opponent of the user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
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

		/// <summary>
		/// Takes two cards from the deck.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Generates a Dobble deck.
		/// The deck is an array of integer lists.
		/// Each list represents a card and the numbers in the list represent the symbols on the card.
		/// There are are ORDER^2 + ORDER + 1 cards in the deck.
		/// each card has ORDER + 1 symbols on it which are chosen from a set of ORDER^2 + ORDER + 1 symbols.
		/// Thus, the lists will contain numbers ranging from 0 to ORDER^2 + ORDER inclusive.
		/// The deck requires that for each pair of cards, there is exactly one symbol that is common between the two cards.
		/// Additionally, to keep the gamee interesting, it is required that for any pair of symbols, there is exactly one card that contains both symbols.
		/// This method uses the following algorithm to generate a deck:
		/// 
		/// 1. Create an array containing all one dimensional subspaces of a three dimensional vector space over a finite field of ORDER elements.
		/// Every element in the array represents some card and some symbol. 
		/// A symbol is said to be on a card if the subspaces representing the card and the symbol are orthogonal.
		/// 2. For each pair of elements in the array (one representing a card and one representing a symbol),
		/// check if the card contains the symbol by computing the dot product. If it does, add that symbol to the card in the final deck.
		/// 3. Return the deck.
		/// </summary>
		/// <returns></returns>
		private static List<int>[] GenerateDobbleDeck()
		{
			Tuple<int, int, int>[] oneDimSubspaces = new Tuple<int, int, int>[GameConfig.ORDER * GameConfig.ORDER + GameConfig.ORDER + 1];
			oneDimSubspaces[0] = new Tuple<int, int, int>(0, 0, 1);
			int count = 1;
			for (int i = 0; i < GameConfig.ORDER; i++)
			{
				for (int j = 0; j < GameConfig.ORDER; j++)
				{
					oneDimSubspaces[count++] = new Tuple<int, int, int>(1, i, j);
				}
				oneDimSubspaces[count++] = new Tuple<int, int, int>(0, 1, i);
			}

			List<int>[] deck = new List<int>[GameConfig.ORDER * GameConfig.ORDER + GameConfig.ORDER + 1];
			for (int i = 0; i < oneDimSubspaces.Length; i++)
			{
				for (int j = 0; j < oneDimSubspaces.Length; j++)
				{
					Tuple<int, int, int> point = oneDimSubspaces[i];
					Tuple<int, int, int> line = oneDimSubspaces[j];
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
