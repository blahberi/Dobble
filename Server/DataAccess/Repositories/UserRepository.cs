using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dobble.Server.DataAccess.Interfaces;
using Dobble.Server.DataAccess.Models;

namespace Dobble.Server.DataAccess.Repositories
{
	internal class UserRepository : RepositoryBase, IUserRepository
	{
		public async Task AddUserAsync(User user)
		{
			string query = "INSERT INTO Users (UserName, PasswordHash, PasswordSalt, Email, FirstName, LastName, Country, City, Gender) " +
				"VALUES (@UserName, @PasswordHash, @PasswordSalt, @Email, @FirstName, @LastName, @Country, @City, @Gender)";

			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
					command.Parameters.Add("@PasswordHash", SqlDbType.Char).Value = user.PasswordHash;
					command.Parameters.Add("@PasswordSalt", SqlDbType.Char).Value = user.PasswordSalt;
					command.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.Email;
					command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = user.FirstName;
					command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
					command.Parameters.Add("@Country", SqlDbType.VarChar).Value = user.Country;
					command.Parameters.Add("@City", SqlDbType.VarChar).Value = user.City;
					command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = user.Gender;

					try
					{
						int rows = await command.ExecuteNonQueryAsync();

						if (rows != 1)
						{
							throw new Exception("Failed to add user to the database");
						}

						Console.WriteLine($"User {user.UserName} added successfully!");
					}
					catch (SqlException ex)
					{
						// Check if the exception is a primary key violation
						if (ex.Number == 2627 || ex.Number == 2601) // 2627 is Constraint Violation, 2601 is Duplicated key row error
						{
							Console.WriteLine("A user with the same ID already exists.");
						}
						else
						{
							Console.WriteLine($"Database error: {ex.Message}");
						}
						throw;
					}
					catch (Exception ex)
					{
						Console.WriteLine($"An error occurred: {ex.Message}");
						throw;
					}
				}
			}
		}

		public Task DeleteUserAsync(string username)
		{
			throw new System.NotImplementedException();
		}

		public async Task<List<User>> GetUsersAsync()
		{
			string query = "SELECT UserName, Email, PasswordHash, PasswordSalt FROM Users";
			List<User> users = new List<User>();

			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								users.Add(new User(
									userName: reader["UserName"].ToString(),
									passwordHash: reader["PasswordHash"].ToString(),
									passwordSalt: reader["PasswordSalt"].ToString(),
									email: reader["Email"].ToString(),
									firstName: reader["FirstName"].ToString(),
									lastName: reader["LastName"].ToString(),
									country: reader["Country"].ToString(),
									city: reader["City"].ToString(),
									gender: reader["Gender"].ToString()));
							}
						}
					}
					catch (SqlException ex)
					{
						Console.WriteLine($"Database error: {ex.Message}");
					}
				}
			}

			return users;
		}

		public async Task<User> GetUserAsync(string userName)
		{
			string query = "SELECT UserName, PasswordHash, PasswordSalt, Email, FirstName, LastName, Country, City, Gender " +
				"FROM Users WHERE UserName = @UserName";
			User user = null;

			using (SqlConnection connection = new SqlConnection(this.ConnectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@UserName", userName);

					try
					{
						using (SqlDataReader reader = await command.ExecuteReaderAsync())
						{
							if (await reader.ReadAsync())
							{
								user = new User(
									userName: reader["UserName"].ToString(),
									passwordHash: reader["PasswordHash"].ToString(),
									passwordSalt: reader["PasswordSalt"].ToString(),
									email: reader["Email"].ToString(),
									firstName: reader["FirstName"].ToString(),
									lastName: reader["LastName"].ToString(),
									country: reader["Country"].ToString(),
									city: reader["City"].ToString(),
									gender: reader["Gender"].ToString());
								Console.WriteLine($"User retrieved: {user.UserName}");
							}
							else
							{
								Console.WriteLine("User not found.");
							}
						}
					}
					catch (SqlException ex)
					{
						Console.WriteLine($"Database error: {ex.Message}");
					}
				}
			}

			return user;
		}

		public Task UpdateUserAsync(User user)
		{
			throw new System.NotImplementedException();
		}
	}
}
