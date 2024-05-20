using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dobble.Client.Forms.Controllers;
using Dobble.Client.Forms.Services;
using Dobble.Client.Forms.Views;
using Dobble.Shared;
using Dobble.Shared.DTOs;
using Dobble.Shared.DTOs.Game;
using Dobble.Shared.Framework;

namespace Dobble.Client.Forms
{
	/// <summary>
	/// Manages the UI of the game.
	/// </summary>
	internal class GameUIManager
	{
		private readonly GameService gameService;
		private readonly IProtocolManager protocolManager;
		private Guid gameId;

		private UserControl activeControl;
		private IProtocolSession session;
		private string invitedUser;

		public GameUIManager()
		{
			this.gameService = new GameService();

			this.activeControl = new ConnectView(this);

			this.gameService.OnGameInvite += this.Game_OnGameInvite;
			this.gameService.OnGameStarted += this.Game_OnGameStarted;
			this.gameService.OnGameNextTurn += this.Game_OnGameNextTurn;
			this.gameService.OnGameOver += this.Game_OnGameOver;

			this.protocolManager = ProtocolHost.CreateDefaultBuilder<ConnectionContext>()
				.RegisterService<IGameService>(this.gameService)
				.RegisterController(Paths.Game, connectionContext => new GameController(connectionContext))
				.Build();

			this.InvitingUsers = new List<InvitingUser>();
		}

		public event Action OnActiveControlChanged;
		public event Action OnGameInvite;
		public event Action OnGameNextTurn;
		public event Action OnGameOver;

		public List<InvitingUser> InvitingUsers { get; }

		public UserControl ActiveControl
		{
			get => this.activeControl;
			private set
			{
				if (this.activeControl != null)
				{
					this.activeControl.Dispose();
				}
				if (this.activeControl != value)
				{
					this.activeControl = value;
					this.OnActiveControlChanged?.Invoke();
				}
			}
		}

		public string UserName { get; private set; }

		// Game properties
		public string OpponentName { get; private set; }
		public int[][] Cards { get; private set; }
		public int YourScore { get; private set; }
		public int OpponentScore { get; private set; }
		public bool YouWonPreviousTurn { get; private set; }
		public int[] PreviousTurnIndices { get; private set; }
		public bool YouWon { get; private set; }


		/// <summary>
		/// Called when the client connects to the server
		/// </summary>
		/// <param name="client"></param>
		/// <param name="communicationStream"></param>
		public void ClientConnected(ISessionStream sessionStream)
		{
			this.ActiveControl = new LoginView(this);
			this.session = this.protocolManager.CreateSession(sessionStream);
			this.gameService.SessionStarted(this.session);
		}

		/// <summary>
		/// Called when the user wants to register a new account
		/// </summary>
		public void ClientRegister()
		{
			this.ActiveControl = new RegisterView(this);
		}

		/// <summary>
		/// Attempts to sign in the user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Task<bool> TrySignIn(string username, string password)
		{
			try
			{
				await this.session.RequestManager.SendUserSignin(username, password);
				this.ClientLoggedIn(username);
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show("Invalid username or password.");
				return false;
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("User is already signed in.");
				return false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Called when the user has finished registering.
		/// </summary>
		private void ClientRegistered()
		{
			this.ClientReturnToLogin();
		}

		/// <summary>
		/// Attempts to register a new user.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="country"></param>
		/// <param name="city"></param>
		/// <param name="gender"></param>
		/// <returns></returns>
		public async Task<bool> TryRegister(
			string username, 
			string password, 
			string email,
			string firstName,
			string lastName,
			string country,
			string city,
			string gender)
		{
			if (!this.ValidateInformation(username, password, email, firstName, lastName, country, city, gender))
			{
				return false;
			}
			try
			{
				await this.session.RequestManager.SendUserRegistration(
					username, 
					password, 
					email,
					firstName,
					lastName,
					country,
					city,
					gender);
				this.ClientRegistered();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// Attemps to invite an opponent to a match.
		/// </summary>
		/// <param name="opponentUsername"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> TryInvite(string opponentUsername, CancellationToken cancellationToken)
		{
			try
			{
				this.invitedUser = opponentUsername;
				Result result = await this.gameService.RequestGameInvite(opponentUsername, cancellationToken);
					
				if (result.Success)
				{
					return true;
				}
				else
				{
					string errorMessage = result.ErrorMessage;
					MessageBox.Show(errorMessage);
					return false;
				}
			}
			catch (OperationCanceledException)
			{
				return false;
			}
			catch (ObjectDisposedException)
			{
				MessageBox.Show($"{opponentUsername} has disconnected");
				return false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				this.invitedUser = null;
			}
		}

		/// <summary>
		/// Signs out the current user.
		/// </summary>
		/// <returns></returns>
		public async Task Signout()
		{
			try
			{
				await this.session.RequestManager.SendUserSignout(this.UserName);
				this.ClientSignedOut();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Removes an inviting user from the list of inviting users.
		/// </summary>
		/// <param name="invitingUser"></param>
		public void RemoveInvitingUser(InvitingUser invitingUser)
		{
			this.InvitingUsers.Remove(invitingUser);
			this.OnGameInvite?.Invoke();
		}

		/// <summary>
		/// Sends the selected pair to the server.
		/// </summary>
		/// <param name="index1"></param>
		/// <param name="index2"></param>
		public void SendPair(int index1, int index2)
		{
			if (this.gameId != Guid.Empty)
			{
				this.gameService.SubmitTurnSelection(index1, index2);
			}
		}

		/// <summary>
		/// Called when the game has finished.
		/// </summary>
		public void ClientGameFinished()
		{
			this.ActiveControl = new GameOverView(this);
		}

		/// <summary>
		/// Called when the client wants to leave the game.
		/// </summary>
		public void ClientLeaveGame()
		{
			this.gameService.LeaveGame();
			this.ClientGameFinished();
		}

		/// <summary>
		/// Called when the client wants to return to the lobby.
		/// </summary>
		public void ClientReturnToLobby()
		{
			this.ActiveControl = new LobbyView(this);
		}

		/// <summary>
		/// Called when the client wants to reutrn to the login screen.
		/// </summary>
		public void ClientReturnToLogin()
		{
			this.ActiveControl = new LoginView(this);
		}

		/// <summary>
		/// Called when the client has logged in.
		/// </summary>
		/// <param name="userName"></param>
		private void ClientLoggedIn(string userName)
		{
			this.UserName = userName;
			this.gameService.SetUserName(userName);

			this.ActiveControl = new LobbyView(this);
		}

		/// <summary>
		/// Called when the client has signed out.
		/// </summary>
		private void ClientSignedOut()
		{
			this.UserName = null;
			this.gameService.SetUserName(null);
			this.ActiveControl = new LoginView(this);
		}

		/// <summary>
		/// Validate information with regex.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="email"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="country"></param>
		/// <param name="city"></param>
		/// <param name="gender"></param>
		/// <returns></returns>
		private bool ValidateInformation(
			string username,
			string password,
			string email,
			string firstName,
			string lastName,
			string country,
			string city,
			string gender)
		{
			if (!InformationValidation.IsValidUsername(username))
			{
				MessageBox.Show("Username must be between 3 and 20 characters long and can only contain letters, numbers and underscores.");
				return false;
			}
			if (!InformationValidation.IsValidPassword(password))
			{
				MessageBox.Show("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.");
				return false;
			}
			if (!InformationValidation.IsValidEmail(email))
			{
				MessageBox.Show("Invalid email address.");
				return false;
			}
			if (!InformationValidation.IsValidName(firstName))
			{
				MessageBox.Show("First name must be between 2 and 20 characters long and can only contain letters.");
				return false;
			}
			if (!InformationValidation.IsValidName(lastName))
			{
				MessageBox.Show("Last name must be between 2 and 20 characters long and can only contain letters.");
				return false;
			}
			if (!InformationValidation.IsValidPlace(country))
			{
				MessageBox.Show("Country must be between 2 and 20 characters long and can only contain letters and spaces.");
				return false;
			}
			if (!InformationValidation.IsValidPlace(city))
			{
				MessageBox.Show("City must be between 2 and 20 characters long and can only contain letters and spaces.");
				return false;
			}
			if (!InformationValidation.IsValidGender(gender))
			{
				MessageBox.Show("Invalid gender.");
				return false;
			}
			return true;
		}

		private void Game_OnGameStarted(object sender, GameStartedEventArgs e)
		{
			this.InvitingUsers.RemoveAll(invitingUser => invitingUser.UserName == e.OpponentName);
			this.InvitingUsers
				.ToArray()
				.ForEach(invitingUser => invitingUser.Reject());

			this.gameId = e.GameId;
			this.OpponentName = e.OpponentName;
			this.Cards = e.Cards;
			this.YourScore = 0;
			this.OpponentScore = 0;
			this.YouWonPreviousTurn = false;
			this.PreviousTurnIndices = null;
			this.ActiveControl = new GameView(this);
		}

		private void Game_OnGameInvite(object sender, GameInviteEventArgs e)
		{
			// If we received an invite by the user we invited, accept it
			if (e.OpponentUserName == this.invitedUser)
			{
				e.GameInviteResponse.SetResult(true);
				return;
			}
			InvitingUser invitingUser = new InvitingUser(this, e);
			this.InvitingUsers.Add(invitingUser);
			this.OnGameInvite?.Invoke();

			e.CancellationToken.Register(() =>
			{
				invitingUser.Cancel();
			});
		}

		private void Game_OnGameNextTurn(object sender, GameNextTurnEventArgs e)
		{
			this.Cards = e.Cards;
			this.YourScore = e.YourScore;
			this.OpponentScore = e.OpponentScore;
			this.YouWonPreviousTurn = e.YouWonPreviousTurn;
			this.PreviousTurnIndices = e.PreviousTurnIndices;
			this.OnGameNextTurn?.Invoke();
		}

		private void Game_OnGameOver(object sender, GameOverEventArgs e)
		{
			this.YouWon = e.YouWon;
			this.YourScore = e.YourScore;
			this.OpponentScore = e.OpponentScore;
			this.YouWonPreviousTurn = e.YouWonPreviousTurn;
			this.PreviousTurnIndices = e.PreviousTurnIndices;
			this.OnGameOver?.Invoke();
		}
	}
}
