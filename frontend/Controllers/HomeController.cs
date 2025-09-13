using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace frontend.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpClientFactory clientFactory;

		public int PageSize = 10;

		public HomeController(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			if (User.Identity?.IsAuthenticated == true)
			{
				HttpClient? client = clientFactory.CreateClient("ApiClient");
				JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

				var articlesWrapper = await client.GetFromJsonAsync<ArticlesWrapper>($"/articles?limit={PageSize}&offset={(page - 1) * PageSize}", options);

				if (articlesWrapper is null)
				{
					ViewBag.Error = "Error while loading articles";
					return View(new FeedViewModel());
				}

				FeedViewModel feed = new()
				{
					Articles = articlesWrapper.Articles,
					PagingInfo = new PagingInfo
					{
						CurrentPage = page,
						ItemsPerPage = PageSize,
						TotalItems = articlesWrapper.ArticlesCount
					}
				};

				return View("Feed", feed);
			}

			return View("Index");
		}
	}
}