using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IArticleService
	{
		Task<ArticleModel?> GetByIdAsync(Guid id);

		Task SaveAsync(ArticleModel model);

		Task<ArticleModel> GetIndependentArticle(string userName, string articleSlug);

		Task<ArticleModel> GetPublicationArticle(string publicationName, string articleSlug);
	}
}
