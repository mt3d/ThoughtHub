namespace ThoughtHub.Api.Models.RecommendedPublishers
{
	public class RecommendedPublisherConnectionModel
	{
		public IReadOnlyList<RecommendedPublisherEdgeModel> Edges { get; set; }
		public PageInfoModel PageInfo { get; set; }
	}
}
