using System.Net.Http.Json;
using System.Text.Json;
using ThoughtHub.Api.Models;

namespace ThoughtHub.UI.BlazorWasm.Services
{
	public class ArticleService(HttpClient httpClient) : IArticleService
	{
		public async Task<IEnumerable<ArticleDigestModel>?> GetFeedArticlesAsync(int startIndex, int size)
		{
			JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

			try
			{
				var articles = await httpClient.GetFromJsonAsync<IEnumerable<ArticleDigestModel>>($"api/recommend/recent_unpersonalized?limit={size}&offset={startIndex}", options);

				return articles;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new List<ArticleDigestModel>();
			}
		}
	}
}
