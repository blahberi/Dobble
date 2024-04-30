using System.Collections.Generic;
using System.Threading.Tasks;
using Dobble.Server.DataAccess.Models;

namespace Dobble.Server.DataAccess.Interfaces
{
	internal interface IUserRepository
	{
		Task<List<User>> GetUsersAsync();
		Task<User> GetUserAsync(string username);
		Task AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(string username);
	}
}
