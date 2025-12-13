using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IArticleService
	{
		Task<ArticleModel?> GetByIdAsync(Guid id);

		public Task SaveAsync(ArticleModel model);
	}
}
