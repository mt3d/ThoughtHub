using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ThoughtHub.Logic.Articles
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
		public List<ArticleModel> Articles { get; set; } = new();
		public int Count { get; set; }
	}

	[Route("[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private PlatformContext context;
		private IMapper mapper;

		public ArticlesController(PlatformContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
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
				.Include(a => a.Publication)
				.Include(a => a.AuthorProfile)
				.ThenInclude(p => p.User)
				.AsNoTracking()
				.ToListAsync();

			List<ArticleModel> articleModels = mapper.Map<List<ArticleModel>>(articles);

			// TODO: Handle tags
			// TODO: Handle author

			return new ArticlesWrapper { Articles = articleModels, Count = query.Count() };
		}

		/// <summary>
		/// Get "For you" articles, which are articles recommended based on the user's reading history.
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		[HttpGet("/for-you")]
		[Authorize]
		public async Task<ArticleWrapper> GetForYou([FromQuery] int? limit, [FromQuery] int? offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get "Featured" articles, which are featured stories from the publications the user follows.
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		[HttpGet("/featured")]
		[Authorize]
		public async Task<ArticleWrapper> GetFeatured([FromQuery] int? limit, [FromQuery] int? offset)
		{
			throw new NotImplementedException();
		}

		///
		///
		[HttpGet("/@{author}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetIndependentArticle(string author, string slug)
		{
			/**
			 * FirstOrDefault() returns the first element or the default value (null for
			 * reference types). Meanwhile, First() throws an exception if no element is found.
			 */
			var article = await context.Articles.AsNoTracking()
				.Include(a => a.Publication)
				.Include(a => a.AuthorProfile)
				.ThenInclude(p => p.User)
				.FirstOrDefaultAsync(x => x.AuthorProfile.User.UserName == author && x.Slug == slug);
			
			if (article == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<ArticleModel>(article));
		}

		[HttpGet("/{publication}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetPublicationArticle(string publication, string slug)
		{
			var article = await context.Articles.AsNoTracking()
				.Include(a => a.Publication)
				.FirstOrDefaultAsync(a =>
					(a.Publication != null && a.Publication.Slug == publication)
					&& a.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<ArticleModel>(article));
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
	}
}
