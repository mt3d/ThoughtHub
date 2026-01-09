using ThoughtHub.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	[Route("api/articles")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private readonly EditorServices.ArticleService _service;
		private readonly ICurrentUserService _currentUserService;
		private readonly IArticleService _articleService;
		private readonly IReadingHistoryService _readingHistoryService;

		public ArticlesController(
			EditorServices.ArticleService service,
			ICurrentUserService currentUserService,
			IArticleService articleService,
			IReadingHistoryService readingHistoryService)
		{
			_service = service;
			_currentUserService = currentUserService;
			_articleService = articleService;
			_readingHistoryService = readingHistoryService;
		}

		// TODO: No need for the endpoint to match
		[HttpGet("@{username}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetIndependentArticle(string userName, string slug)
		{
			var article = await _articleService.GetIndependentArticleAsync(userName, slug);

			if (article is null)
			{
				return NotFound();
			}

			var profile = await _currentUserService.GetProfileAsync();

			await _readingHistoryService.UpdateArticleHistory(article.Id, profile.Id);

			return Ok(article);
		}

		[HttpGet("{publication}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetPublicationArticle(string publication, string slug)
		{
			var article = await _articleService.GetPublicationArticleAsync(publication, slug);

			if (article is null)
			{
				return NotFound();
			}

			return Ok(article);
		}

		/// <summary>
		/// Retreive all the articles published in the specified publication.
		/// 
		/// Usage: From the publication page, under Archives.
		/// </summary>
		/// <param name="publicationId"></param>
		/// <param name="year"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IActionResult> GetPublicationArticles(int publicationId, int? year)
		{
			throw new NotImplementedException();
		}

		[Route("save")]
		[HttpPost]
		[Authorize] // TODO: Check permissions for posting articles
		public async Task<ArticleEditModel> Save(ArticleEditModel model)
		{
			model.AuthorProfileId = (await _currentUserService.GetProfileAsync()).Id;

			if (string.IsNullOrEmpty(model.Published))
			{
				model.Published = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
			}

			var result = await Save(model, false);

			// TODO: Update all clients

			return result;
		}

		// Save for drafts, publish for anything else, can be used to unpublish articles.
		private async Task<ArticleEditModel> Save(ArticleEditModel model, bool draft = false)
		{
			try
			{
				await _service.Save(model, draft);
			}
			catch // TODO: Catch any validation exceptions
			{
				model.Status = new StatusMessage
				{
					// TODO: Edit once exceptions are created.
					Type = "Erorr",
					Body = "Error"
				};

				return model; // Will be returned as json
			}

			var result = await _service.GetById(model.Id);
			result.Status = new StatusMessage
			{
				Type = "Success",
				Body = "The article was successfully saved"
			};

			return result;
		}
	}
}
