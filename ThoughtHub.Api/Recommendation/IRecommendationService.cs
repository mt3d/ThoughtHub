using System.Security.Claims;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.Recommendation
{
	public interface IRecommendationService
	{
		Task<IEnumerable<TagModel>> SuggestTopics(Guid profileId, int count = 3);

		Task<RecommendedPublisherConnectionModel> GetRecommendedPublishersAsync(int count, string? after, ClaimsPrincipal user);

		Task<IEnumerable<UserPublisherModel>> SuggestProfiles(Guid profileId, int count = 3);

		Task<IEnumerable<PublicationPublisherModel>> SuggestPublications(Guid profileId, int count = 3);
	}
}
