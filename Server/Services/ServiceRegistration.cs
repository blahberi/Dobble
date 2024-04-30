using Dobble.Server.DataAccess;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	/// <summary>
	/// A static class for registering services.
	/// The method extends the protocol manager builder and registers all the server services onto the protocol manager.
	/// </summary>
	internal static class ServiceRegistration
	{
		public static IProtocolManagerBuilder<ServerConnectionContext> RegisterServices(this IProtocolManagerBuilder<ServerConnectionContext> builder)
		{
			SqlDataAccess dataAccess = new SqlDataAccess();
			IUserService userService = new UserService(dataAccess);

			IGameService gameService = new GameService(userService);

			return builder
				.RegisterService(userService)
				.RegisterService(gameService);
		}
	}
}
