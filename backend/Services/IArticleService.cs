using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IArticleService
	{
		Task<ArticleModel?> GetByIdAsync(Guid id);

		Task SaveAsync(ArticleModel model);

		Task<ArticleModel?> GetIndependentArticleAsync(string userName, string articleSlug);

		Task<ArticleModel?> GetPublicationArticleAsync(string publicationName, string articleSlug);
	}
}
