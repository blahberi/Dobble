using Dobble.Shared.DTOs;
using Dobble.Shared.Framework;

namespace Dobble.Server.Controllers
{
	internal static class ControllerRegistration
	{
		public static IProtocolManagerBuilder<ServerConnectionContext> RegisterControllers(this IProtocolManagerBuilder<ServerConnectionContext> builder)
		{
			return builder
				.RegisterController(Paths.Users, connectionContext => new UserController(connectionContext))
				.RegisterController(Paths.Game, connectionContext => new GameController(connectionContext));
		}
	}
}
