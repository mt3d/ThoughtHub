using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	public class Profile
	{
		public int ProfileId { get; set; }

		public string? Username { get; set; }

		public string? FullName { get; set; }

		public string? Email { get; set; }

		public string? Bio { get; set; }

		public string? ProfilePic { get; set; }

		public string? Url { get; set; }

		[JsonIgnore]
		public List<FollowMapping> Followers { get; init; } = new();

		[JsonIgnore]
		public List<FollowMapping> Followees { get; init; } = new();
	}
}
