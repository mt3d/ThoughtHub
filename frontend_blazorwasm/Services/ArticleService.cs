using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.UI.BlazorWasm.Services
{
	public class ArticleService(HttpClient httpClient) : IArticleService
	{
		JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

		public async Task<ArticleModel> GetIndependentArticleAsync(string author, string slug)
		{
			return await httpClient.GetFromJsonAsync<ArticleModel>($"api/articles/@{author}/{slug}", options);
		}

		public async Task<ArticleModel> GetPublicationArticleAsync(string publication, string slug)
		{
			return await httpClient.GetFromJsonAsync<ArticleModel>($"api/articles/{publication}/{slug}", options);
		}

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
				return new List<ArticleDigestModel>();
			}
		}

		public async Task<RecommendedPublisherConnectionModel> GetWhoToFollowConnectionAsync()
		{
			return await httpClient.GetFromJsonAsync<RecommendedPublisherConnectionModel>("api/recommend/who-to-follow?count=3");
		}
	}
}
