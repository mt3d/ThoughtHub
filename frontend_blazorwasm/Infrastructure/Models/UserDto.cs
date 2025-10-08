namespace frontend_blazorwasm.Infrastructure.Models
{
	public class UserDto
	{
		public string? Username { get; init; }

		public string? Email { get; init; }

		public string? Bio { get; init; }

		public string? ProfilePic { get; init; }

		// JWT token
		public string? Token { get; set; }
	}
}
