using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	[ApiController]
	[Route("api/reading-lists")]
	public class ReadingListsController : ControllerBase
	{
		private readonly IReadingListService _readingListService;
		private readonly IArticleService _articleService;
		private readonly ICurrentUserService _currentUserService;

		public ReadingListsController(
			IReadingListService readingListService,
			IArticleService articleService)
		{
			_readingListService = readingListService;
			_articleService = articleService;
		}

		[Authorize]
		[HttpPost("/default/articles")]
		public async Task<IActionResult> AddArticleToReadingList(Guid articleId)
		{
			if (articleId == Guid.Empty)
			{
				return NotFound();
			}

			var article = await _articleService.GetByIdAsync(articleId);
			if (article is null)
			{
				return NotFound();
			}

			var profile = await _currentUserService.GetProfileAsync();

			await _readingListService.AddArticleToReadingList(profile.Id, articleId);

			return NoContent();
		}
	}
}
