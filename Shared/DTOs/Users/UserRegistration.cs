namespace Dobble.Shared.DTOs.Users
{
	/// <summary>
	/// DTO of a user registration message.
	/// </summary>
	public class UserRegistration
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Gender { get; set; }
	}
}
