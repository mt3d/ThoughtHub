using ThoughtHub.Data.Identity;
using System.Text.Json.Serialization;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Data.Entities
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

		// TODO: Handle images.
		//public string? ProfilePic { get; set; }

		[JsonIgnore]
		public List<FollowMapping> Followers { get; init; } = new();

		[JsonIgnore]
		public List<FollowMapping> Followees { get; init; } = new();

		/// <summary>
		/// Gets or sets the articles published by this profile.
		/// </summary>
		public ICollection<Article> Articles { get; set; } = new List<Article>();

		public ICollection<Publication> MemberPublications { get; set; } = new List<Publication>();

		public ICollection<Publication> FollowedPublications { get; set; } = new List<Publication>();

		/// <summary>
		/// Provides access to the joint table of the membership relationship.
		/// </summary>
		public ICollection<PublicationMember> PublicationMemberships { get; set; } = new List<PublicationMember>();

		/// <summary>
		/// Provides access to the joint table of the following relationship.
		/// </summary>
		public ICollection<PublicationFollower> PublicationFollowers { get; set; } = new List<PublicationFollower>();
	}
}
