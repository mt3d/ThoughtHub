using ThoughtHub.Api.Models;

namespace ThoughtHub.Recommendation
{
	public interface IRecommendationService
	{
		Task<IEnumerable<TagModel>> SuggestTopics(int profileId, int count = 3);
	}
}
