using System.Text.RegularExpressions;
using Dobble.Shared;

namespace Dobble.Server.DataAccess.Models
{
	internal class User
	{
		public User(
			string userName, 
			string passwordHash, 
			string passwordSalt, 
			string email,
			string firstName,
			string lastName,
			string country,
			string city,
			string gender)
		{
			this.UserName = userName;
			this.PasswordHash = passwordHash;
			this.PasswordSalt = passwordSalt;
			this.Email = email;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Country = country;
			this.City = city;
			this.Gender = gender;
		}

		public string UserName { get; }
		public string PasswordHash { get; }
		public string PasswordSalt { get; }
		public string Email { get; }
		public string FirstName { get; }
		public string LastName { get; }
		public string Country { get; }
		public string City { get; }
		public string Gender { get; }

		public bool IsValidInformation()
		{
			return InformationValidation.IsValidUsername(this.UserName)
				&& InformationValidation.IsValidPassword(this.PasswordHash)
				&& InformationValidation.IsValidEmail(this.Email)
				&& InformationValidation.IsValidName(this.FirstName)
				&& InformationValidation.IsValidName(this.LastName)
				&& InformationValidation.IsValidPlace(this.Country)
				&& InformationValidation.IsValidPlace(this.City)
				&& InformationValidation.IsValidGender(this.Gender);
		}
	}
}
