using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.UI.BlazorWasm.Services
{
	public interface IArticleService
	{
		Task<IEnumerable<ArticleDigestModel>?> GetFeedArticlesAsync(int startIndex, int size);

		Task<RecommendedPublisherConnectionModel> GetWhoToFollowConnectionAsync();

		Task<ArticleModel> GetIndependentArticleAsync(string author, string slug);

		Task<ArticleModel> GetPublicationArticleAsync(string publication, string slug);
	}
}
