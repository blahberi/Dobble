namespace Dobble.Shared.DTOs
{
	/// <summary>
	/// All the methods that are used in this project
	/// </summary>
	public static class Methods
	{
		/// <summary>
		/// These are methods that are sent by the client for user specific operations
		/// </summary>
		public static class Users
		{
			public const string Register = nameof(Register);
			public const string Signin = nameof(Signin);
			public const string Signout = nameof(Signout);
		}

		/// <summary>
		/// These are methods that are sent by the client for game operations
		/// </summary>
		public static class GameServer
		{
			public const string Invite = nameof(Invite);
			public const string Leave = nameof(Leave);
			public const string TurnSelection = nameof(TurnSelection);
		}

		/// <summary>
		/// These are methods that are sent by the server for game operations
		/// </summary>
		public static class GameClient
		{
			public const string Invite = nameof(Invite);
			public const string NextTurn = nameof(NextTurn);
			public const string GameOver = nameof(GameOver);
		}

		/// <summary>
		/// This is the respone method that is sent as a response to a different request
		/// </summary>
		internal static class Response
		{
			public const string Success = nameof(Success);
			public const string Failure = nameof(Failure);
		}

		/// <summary>
		/// This is the cancellation method that is sent to cancel a request
		/// </summary>
		internal static class Cancellation
		{
			public const string Cancel = nameof(Cancel);
		}
	}
}
