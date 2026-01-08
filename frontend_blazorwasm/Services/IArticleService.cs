using ThoughtHub.Api.Models;

namespace ThoughtHub.UI.BlazorWasm.Services
{
	public interface IArticleService
	{
		Task<IEnumerable<ArticleDigestModel>?> GetFeedArticlesAsync(int startIndex, int size);
	}
}
