namespace ThoughtHub.Api.Models
{
	public class AuthorModel
	{
		public string Name { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string FullName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		//public string? Bio { get; set; } = string.Empty;

		public string? ProfilePic { get; set; } = string.Empty;

		//[JsonIgnore]
		//public bool Following { get; set; }

		//[JsonIgnore]
		//public int FollowersCount { get; set; }

		//[JsonIgnore]
		//public int FollowingCount { get; set; }

		//[JsonIgnore]
		//public string ProfileDescription { get; set; } = string.Empty;
	}
}
