using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace backend.Data.Identity
{
	public class User : IdentityUser
	{
		// public string? UserName { get; set; }

		public string? FullName { get; set; }

		// public string? Email { get; set; }

		public string? Bio { get; set; }

		public string? ProfilePic { get; set; }

		public string? Url { get; set; }
	}
}
