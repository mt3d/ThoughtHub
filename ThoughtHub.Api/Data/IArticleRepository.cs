using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Data
{
	public interface IArticleRepository
	{
		public Task<ArticleModel?> GetById(Guid id);

		public Task SaveAsync(ArticleModel model);
	}
}
