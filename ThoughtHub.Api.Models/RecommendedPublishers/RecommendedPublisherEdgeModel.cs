namespace ThoughtHub.Api.Models.RecommendedPublishers
{
	public class RecommendedPublisherEdgeModel
	{
		public FollowablePublisherModel Node { get; init; }
		public string Cursor { get; init; }
	}
}
