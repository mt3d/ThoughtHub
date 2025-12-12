using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IArticleService
	{
		Task<ArticleM?> GetByIdAsync(Guid id);

		public Task SaveAsync(ArticleM model);
	}
}
