using backend.Data;
using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Logic.Articles
{
	/*
	 * A wrapper/envelope is used add additional metadata to the actual payload, such as
	 * information about the response status, errors, and other contextual details.
	 * 
	 * Records are primarily intended for supporting immutable data models.
	 */
	public record ArticleWrapper(Article Article);

	public class ArticlesWrapper
	{
		public List<Article> Articles { get; set; } = new();
		public int Count { get; set; }
	}

	[Route("[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private PlatformContext context;

		public ArticlesController(PlatformContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Get a list of Articles
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ArticlesWrapper> Get(
			[FromQuery] string? tag,
			[FromQuery] string? author,
			[FromQuery] int? limit,
			[FromQuery] int? offset)
		{
			// TODO: Include Authors, ArticleFavorites, and Article Tags
			var query = context.Articles.AsNoTracking();

			var articles = await query
				.OrderByDescending(a => a.CreatedAt)
				.Skip(offset ?? 0)
				.Take(limit ?? 10)
				.AsNoTracking()
				.ToListAsync();

			// TODO: Handle tags
			// TODO: Handle author

			return new ArticlesWrapper { Articles = articles, Count = query.Count() };
		}

		[HttpGet("{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(string slug)
		{
			var article = await context.Articles.AsNoTracking()
				.Include(a => a.Publication)
				.Include(a => a.Author)
				.FirstOrDefaultAsync(x => x.Slug == slug);
			
			if (article == null)
			{
				return NotFound();
			}

			return Ok(article);
		}
	}
}
