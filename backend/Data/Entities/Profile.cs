using backend.Data.Identity;
using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	public class Profile
	{
		public int ProfileId { get; set; }

		/// <summary>
		/// Identity User Id's are strings.
		/// </summary>
		public string? UserId { get; set; }

		public User? User { get; set; }

		public string? FullName { get; set; }

		public string? Bio { get; set; }

		public string? ProfilePic { get; set; }

		// TODO: Remove. The Url is just the username, and usernames are unique.
		//public string? Url { get; set; }

		[JsonIgnore]
		public List<FollowMapping> Followers { get; init; } = new();

		[JsonIgnore]
		public List<FollowMapping> Followees { get; init; } = new();
	}
}
