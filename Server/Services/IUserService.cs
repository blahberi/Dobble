using System.Threading.Tasks;
using Dobble.Server.DataAccess.Models;
using Dobble.Shared.DTOs.Users;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	internal interface IUserService
	{
		Task<Result> RegisterUser(UserRegistration userRegistration);

		Task<Result<User>> SigninUser(ServerConnectionContext connectionContext, UserSignin userSignin);

		void SignoutUser(User user);

		bool TryGetSignedinUserConnectionContext(string username, out ServerConnectionContext userConnectionContext);
	}
}
