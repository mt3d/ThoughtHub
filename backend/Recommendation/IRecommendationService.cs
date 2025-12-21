using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.Recommendation
{
	public interface IRecommendationService
	{
		Task<IEnumerable<TagModel>> SuggestTopics(int profileId, int count = 3);

		Task<IEnumerable<FollowablePublisherModel>> GetRecommendedPublishersAsync(int profileId, int count = 3);

		Task<IEnumerable<UserPublisherModel>> SuggestProfiles(int profileId, int count = 3);

		Task<IEnumerable<PublicationPublisherModel>> SuggestPublication(int profileId, int count = 3);
	}
}
