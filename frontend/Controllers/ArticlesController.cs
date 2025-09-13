using frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace frontend.Controllers
{
	class ArticleWrapper
	{
		public Article Article;
	}

	[Route("articles")]
	public class ArticlesController : Controller
	{
		private readonly IHttpClientFactory clientFactory;

		public ArticlesController(IHttpClientFactory clientFactory)
		{
			this.clientFactory = clientFactory;
		}

		[HttpGet("{slug}")]
		public async Task<IActionResult> Details(string slug)
		{
			HttpClient? client = clientFactory.CreateClient("ApiClient");
			JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

			Article? article = await client.GetFromJsonAsync<Article>($"/articles/{slug}", options);

			if (article is null)
			{
				// TODO: Choose a better approach.
				ViewBag.Error = "Error while loading the article";
				return View();
			}

			// Build replies tree

			// TODO: Handle comments errors.
			List<CommentDto>? comments = await client.GetFromJsonAsync<List<CommentDto>>($"/articles/{slug}/comments", options);
			ILookup<int?, CommentDto> commentLookup = comments.OrderBy(c => c.CreatedAt).ToLookup(c => c.ParentCommentId);
			List<CommentDto> topLevel = commentLookup[null].ToList();

			void BuildTree(CommentDto parent)
			{
				parent.Replies = commentLookup[parent.CommentId].ToList();

				foreach (var reply in parent.Replies)
				{
					BuildTree(reply);
				}
			}

			foreach (var comment in topLevel)
			{
				BuildTree(comment);
			}

			article.Comments = comments.ToList();

			return View(new ArticleWrapper { Article = article });
		}

		[HttpPost("{slug}")]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> AddComment(string slug, [FromBody] CommentDto model)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Details", new { slug });
			}

			var token = User.FindFirst("access_token")?.Value;
			var client = clientFactory.CreateClient("ApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await client.PostAsJsonAsync($"/articles/{slug}/comments", model.BodySource);

			if (!response.IsSuccessStatusCode)
			{
				// TODO: Log the api error.

				TempData["CommentFailedMessage"] = "Sorry, we couldn't post your comment. Please try again.";

				return RedirectToAction("Details", new { slug });
			}

			return RedirectToAction("Details", new { slug });
		}
	}
}
