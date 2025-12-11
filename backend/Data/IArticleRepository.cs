using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Data
{
	public interface IArticleRepository
	{
		public Task<ArticleM?> GetById(Guid id);

		public Task SaveAsync(ArticleM model);
	}
}
