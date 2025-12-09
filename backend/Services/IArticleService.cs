using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IArticleService
	{
		//Task<> GetByIdAsync(Guid id);

		public Task SaveAsync(ArticleM model);
	}
}
