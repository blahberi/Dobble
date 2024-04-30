using Dobble.Client.Controllers;
using Dobble.Client.Services;
using Dobble.Shared;
using Dobble.Shared.DTOs;
using Dobble.Shared.Framework;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private readonly IGame game;
        private readonly string userName;
        private readonly string password;
        private readonly string email;
        private readonly IProtocolManager protocolManager;

        public Program(IGameService gameService, IGame game, string userName, string password, string email)
        {
            this.game = game;
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.protocolManager = ProtocolHost.CreateDefaultBuilder<ConnectionContext>()
                .RegisterService(gameService)
                .RegisterController(Paths.Game, connectionContext => new GameController(connectionContext))
                .Build();
        }

        static async Task Main(string[] args)
        {
            string userName = args[0];
            string password = args[1];
            string email = args[2];

            GameService gameService = new GameService(userName);

            Program program = new Program(gameService, gameService, userName, password, email);

            await program.Start();
        }

        private async Task Start()
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync("localhost", 3005);
                Console.WriteLine("Connected to server.");

                using (IProtocolSession session = this.protocolManager.CreateSession(client))
                {
                    this.game.SessionStarted(session);

                    if (!await SignIn(session, this.userName, this.password, this.email))
                    {
                        Console.WriteLine("Bye Bye.");
                        return;
                    }

                    await this.PlayGame();
                    await session.WaitForSessionToEnd();
                }
            }
        }

        private async Task PlayGame()
        {
            this.game.OnGameInvite += this.GameService_OnGameInvite;
            this.game.OnGameStarted += this.GameService_OnGameStarted;
            this.game.OnGameNextTurn += this.GameService_OnGameNextTurn;
            this.game.OnGameOver += this.GameService_OnGameOver;

            while (true)
            {
                Console.WriteLine("Enter opponent username. or nothing to wait for another player to start:");
                string opponent = await ReadLineAsync();

                if (string.IsNullOrEmpty(opponent))
                {
                    return;
                }

                Result result = await this.game.RequestGameInvite(opponent);
                if (result.Success)
                {
                    Console.WriteLine($"Game invite accepted by {opponent}");
                    return;
                }

                Console.Write(result.ErrorMessage);
            }
        }

        private void GameService_OnGameStarted(object sender, GameStartedEventArgs e)
        {
            Console.WriteLine($"Game started. Opponent: {e.OpponentName}");

            this.PrintCardsAndWaitForSelection(e.Cards);
        }

        private void GameService_OnGameOver(object sender, GameOverEventArgs e)
        {
            Console.WriteLine($"Game over. Winner: {e.Winner}");
        }

        private void GameService_OnGameNextTurn(object sender, GameNextTurnEventArgs e)
        {
            Console.WriteLine($"You {(e.YouWonPreviousTurn ? "Won" : "Lost")} the turn.");
            Console.WriteLine($"Current Score: You: {e.YourScore} Opponent: {e.OpponentScore}");
            this.PrintCardsAndWaitForSelection(e.Cards);
        }

        private async void GameService_OnGameInvite(object sender, GameInviteEventArgs e)
        {
            Console.WriteLine($"The user {e.OpponentUserName} would like to play with you.Do you accept ? (Y/N)");
            string response = await ReadLineAsync();

            e.GameInviteResponse.SetResult(response == "Y");
        }

        private async void PrintCardsAndWaitForSelection(int[][] cards)
        {
            Console.WriteLine($"Card1: {string.Join(", ", cards[0])}");
            Console.WriteLine($"Card2: {string.Join(", ", cards[1])}");
            Console.WriteLine("Enter Two Numbers");
            string[] Indices = (await ReadLineAsync()).Split(' ');

            await this.game.UpdateTurnSelection(int.Parse(Indices[0]), int.Parse(Indices[1]));
        }

        private static async Task<bool> SignIn(IProtocolSession session, string userName, string password, string email)
        {
            bool signIn = await TrySignIn(session, userName, password);

            if (signIn == false)
            {
                bool register = await TryRegister(session, userName, password, email);
                if (register == false)
                {
                    Console.WriteLine("Could not sign in or register. Done.");
                    return false;
                }

                signIn = await TrySignIn(session, userName, password);
                if (signIn == false)
                {
                    Console.WriteLine("Could not signin. Done.");
                    return false;
                }
            }

            return true;
        }

        private static async Task<string> ReadLineAsync()
        {
            return await Task.Run(() => Console.ReadLine());
        }

        private static async Task<bool> TrySignIn(IProtocolSession session, string username, string password)
        {
            try
            {
                Console.WriteLine($"Signin user {username}");
                await session.RequestManager.SendUserSignin(username, password);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Signin unauthorized");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static async Task<bool> TryRegister(IProtocolSession session, string username, string password, string email)
        {
            try
            {
                Console.WriteLine($"Registering user {username}");
                await session.RequestManager.SendUserRegistration(username, password, email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

