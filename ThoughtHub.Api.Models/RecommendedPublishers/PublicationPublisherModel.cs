namespace ThoughtHub.Api.Models.RecommendedPublishers
{
	public sealed class PublicationPublisherModel : FollowablePublisherModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public string Domain { get; set; }
		public string PublicationImageUrl { get; set; } = string.Empty;
	}
}
