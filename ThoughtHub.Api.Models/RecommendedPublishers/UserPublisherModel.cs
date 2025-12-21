namespace ThoughtHub.Api.Models.RecommendedPublishers
{
	// The type is complete enough to render a follow card without additional API calls.
	public sealed class UserPublisherModel : FollowablePublisherModel
	{
		public string Name { get; set; }
		public string? Bio { get; set; }
		public string Username { get; set; }

		//public string Url { get; set; }

		public string ProfilePictureUrl { get; init; } = string.Empty;

		// TODO: Add membership info (like "Friend of Medium"), verification models
		// (like "Book Author", custom domains, and newsletters.
	}
}
