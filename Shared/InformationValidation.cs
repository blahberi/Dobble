using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dobble.Shared
{
	/// <summary>
	/// A class that contains methods to validate information
	/// </summary>
	public static class InformationValidation
	{
		public static Regex usernameRegex = new Regex(@"^[a-zA-Z0-9_]{3,20}$");
		public static Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,64}$");
		public static Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
		public static Regex nameRegex = new Regex(@"^[a-zA-Z]{2,20}$");
		public static Regex placeRegex = new Regex(@"^[a-zA-Z ]{2,20}$");
		public static Regex cityRegex = new Regex(@"^[a-zA-Z ]{2,20}$");
		public static Regex genderRegex = new Regex(@"^(Male|Female|Chad|Other)$");

		/// <summary>
		/// Checks if the username is valid
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		public static bool IsValidUsername(string username)
		{
			return usernameRegex.IsMatch(username);
		}

		/// <summary>
		/// Checks if the password is valid
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public static bool IsValidPassword(string password)
		{
			return passwordRegex.IsMatch(password);
		}

		/// <summary>
		/// Checks if the email is valid
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static bool IsValidEmail(string email)
		{
			return emailRegex.IsMatch(email);
		}

		/// <summary>
		/// Checks if the name is valid
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool IsValidName(string name)
		{
			return nameRegex.IsMatch(name);
		}

		/// <summary>
		/// Checks if the city is valid
		/// </summary>
		/// <param name="place"></param>
		/// <returns></returns>
		public static bool IsValidPlace(string place)
		{
			return placeRegex.IsMatch(place);
		}

		/// <summary>
		/// Checks if the city is valid
		/// </summary>
		/// <param name="gender"></param>
		/// <returns></returns>
		public static bool IsValidGender(string gender)
		{
			return genderRegex.IsMatch(gender);
		}
	}
}
