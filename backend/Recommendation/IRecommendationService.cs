using System.Security.Claims;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.Recommendation
{
	public interface IRecommendationService
	{
		Task<IEnumerable<TagModel>> SuggestTopics(int profileId, int count = 3);

		Task<RecommendedPublisherConnectionModel> GetRecommendedPublishersAsync(int count, string? after, ClaimsPrincipal user);

		Task<IEnumerable<UserPublisherModel>> SuggestProfiles(int profileId, int count = 3);

		Task<IEnumerable<PublicationPublisherModel>> SuggestPublications(int profileId, int count = 3);
	}
}
