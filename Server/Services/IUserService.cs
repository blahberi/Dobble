using System.Threading.Tasks;
using Dobble.Server.DataAccess.Models;
using Dobble.Shared.DTOs.Users;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	/// <summary>
	/// Interface for the user service.
	/// </summary>
	internal interface IUserService
	{
		/// <summary>
		/// Registers a new user.
		/// </summary>
		/// <param name="userRegistration"></param>
		/// <returns></returns>
		Task<Result> RegisterUser(UserRegistration userRegistration);

		/// <summary>
		/// Signs in a user.
		/// </summary>
		/// <param name="connectionContext"></param>
		/// <param name="userSignin"></param>
		/// <returns></returns>
		Task<Result<User>> SigninUser(ServerConnectionContext connectionContext, UserSignin userSignin);

		/// <summary>
		/// Signs out a user.
		/// </summary>
		/// <param name="user"></param>
		void SignoutUser(User user);

		/// <summary>
		/// Tries to get the signed in user connection context.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="userConnectionContext"></param>
		/// <returns></returns>
		bool TryGetSignedinUserConnectionContext(string username, out ServerConnectionContext userConnectionContext);
	}
}
