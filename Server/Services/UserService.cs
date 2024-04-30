using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dobble.Server.DataAccess;
using Dobble.Server.DataAccess.Models;
using Dobble.Shared.DTOs.Users;
using Dobble.Shared.Framework;

namespace Dobble.Server.Services
{
	internal class UserService : IUserService
	{
		private readonly IDataAccess dataAccess;
		private readonly Dictionary<string, ServerConnectionContext> signedInUsers;

		public UserService(IDataAccess dataAccess)
		{
			this.dataAccess = dataAccess;
			this.signedInUsers = new Dictionary<string, ServerConnectionContext>();
		}

		public async Task<Result> RegisterUser(UserRegistration userRegistration)
		{
			try
			{
				(string passwordHash, string salt) = HashPassword(userRegistration.Password);

				User user = new User(
					userRegistration.Username, 
					passwordHash, 
					salt, 
					userRegistration.Email, 
					userRegistration.FirstName, 
					userRegistration.LastName, 
					userRegistration.Country, 
					userRegistration.City, 
					userRegistration.Gender);

				if (!user.IsValidInformation())
				{
					return Result.FailureResult("Invalid user data");
				}

				await this.dataAccess.UserRepository.AddUserAsync(user);

				return Result.SuccessResult();
			}
			catch
			{
				return Result.FailureResult("Could not register the user");
			}
		}

		public async Task<Result<User>> SigninUser(ServerConnectionContext connectionContext, UserSignin userSignin)
		{
			User user = await this.dataAccess.UserRepository.GetUserAsync(userSignin.Username);
			if (user == null)
			{
				return Result<User>.FailureResult("Could not find user in the database");
			}

			string computedHash = ComputeHash(userSignin.Password, user.PasswordSalt);

			if (computedHash != user.PasswordHash)
			{
				return Result<User>.FailureResult("Passwords don't match");
			}

			lock (this.signedInUsers)
			{
				// Check if the user is already signed in
				if (this.signedInUsers.TryGetValue(userSignin.Username, out ServerConnectionContext signedInUser))
				{
					user = signedInUser.User;
				}
				else
				{
					// Indicate user is now signed in and store its model
					connectionContext.User = user;
					this.signedInUsers.Add(userSignin.Username, connectionContext);
				}
			}

			return Result<User>.SuccessResult(user);
		}

		public void SignoutUser(User user)
		{
			lock (this.signedInUsers)
			{
				this.signedInUsers.Remove(user.UserName);
			}
		}

		public bool TryGetSignedinUserConnectionContext(string username, out ServerConnectionContext userConnectionContext)
		{
			return this.signedInUsers.TryGetValue(username, out userConnectionContext);
		}

		private static (string, string) HashPassword(string password)
		{
			byte[] saltBytes = new byte[16];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(saltBytes);
			}
			string saltText = Convert.ToBase64String(saltBytes);
			string hash = ComputeHash(password, saltText);
			return (hash, saltText);
		}

		private static string ComputeHash(string password, string salt)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] saltedPassword = Encoding.UTF8.GetBytes(salt + password);
				byte[] hash = sha256.ComputeHash(saltedPassword);
				return Convert.ToBase64String(hash);
			}
		}
	}
}
