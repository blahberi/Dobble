namespace Dobble.Shared.DTOs
{
	public static class Methods
	{
		public static class Users
		{
			public const string Register = nameof(Register);
			public const string Signin = nameof(Signin);
			public const string Signout = nameof(Signout);
		}

		public static class GameServer
		{
			public const string Invite = nameof(Invite);
			public const string Leave = nameof(Leave);
			public const string TurnSelection = nameof(TurnSelection);
		}

		public static class GameClient
		{
			public const string Invite = nameof(Invite);
			public const string NextTurn = nameof(NextTurn);
			public const string GameOver = nameof(GameOver);
		}

		internal static class Response
		{
			public const string Success = nameof(Success);
			public const string Failure = nameof(Failure);
		}

		internal static class Cancellation
		{
			public const string Cancel = nameof(Cancel);
		}
	}
}
