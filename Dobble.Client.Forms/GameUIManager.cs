using System;
using System.Collections.Generic;
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


		public void ClientConnected(TcpClient client)
		{
			this.ActiveControl = new LoginView(this);
			this.session = this.protocolManager.CreateSession(client);
			this.gameService.SessionStarted(this.session);
		}

		public void ClientRegister()
		{
			this.ActiveControl = new RegisterView(this);
		}

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
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void ClientRegistered()
		{
			this.ClientReturnToLogin();
		}

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

		public void RemoveUser(InvitingUser invitingUser)
		{
			this.InvitingUsers.Remove(invitingUser);
			this.OnGameInvite?.Invoke();
		}

		public void SendPair(int index1, int index2)
		{
			if (this.gameId != Guid.Empty)
			{
				this.gameService.UpdateTurnSelection(index1, index2);
			}
		}

		public void ClientGameFinished()
		{
			this.ActiveControl = new GameOverView(this);
		}

		public void ClientLeaveGame()
		{
			this.gameService.LeaveGame();
			this.ClientGameFinished();
		}

		public void ClientReturnToLobby()
		{
			this.ActiveControl = new LobbyView(this);
		}

		public void ClientReturnToLogin()
		{
			this.ActiveControl = new LoginView(this);
		}

		private void ClientLoggedIn(string userName)
		{
			this.UserName = userName;
			this.gameService.SetUserName(userName);

			this.ActiveControl = new LobbyView(this);
		}

		private void ClientSignedOut()
		{
			this.UserName = null;
			this.gameService.SetUserName(null);
		}

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
