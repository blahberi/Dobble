using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dobble.Shared
{
	public static class InformationValidation
	{
		public static Regex usernameRegex = new Regex(@"^[a-zA-Z0-9_]{3,20}$");
		public static Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
		public static Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
		public static Regex nameRegex = new Regex(@"^[a-zA-Z]{2,20}$");
		public static Regex placeRegex = new Regex(@"^[a-zA-Z ]{2,20}$");
		public static Regex cityRegex = new Regex(@"^[a-zA-Z ]{2,20}$");
		public static Regex genderRegex = new Regex(@"^(Male|Female|Chad|Other)$");

		public static bool IsValidUsername(string username)
		{
			return usernameRegex.IsMatch(username);
		}
		public static bool IsValidPassword(string password)
		{
			return passwordRegex.IsMatch(password);
		}
		public static bool IsValidEmail(string email)
		{
			return emailRegex.IsMatch(email);
		}
		public static bool IsValidName(string name)
		{
			return nameRegex.IsMatch(name);
		}
		public static bool IsValidPlace(string place)
		{
			return placeRegex.IsMatch(place);
		}
		public static bool IsValidGender(string gender)
		{
			return genderRegex.IsMatch(gender);
		}
	}
}
