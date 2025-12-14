using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	[Route("api/articles")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private PlatformContext context;
		private IMapper mapper;
		private readonly EditorServices.ArticleService _service;
		private readonly ICurrentUserService _currentUserService;

		public ArticlesController(
			PlatformContext context,
			IMapper mapper,
			EditorServices.ArticleService service,
			ICurrentUserService currentUserService)
		{
			this.context = context;
			this.mapper = mapper;
			_service = service;
			_currentUserService = currentUserService;
		}

		/// <summary>
		/// Get a list of Articles
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IList<ArticleCardModel>> Get(
			[FromQuery] string? tag,
			[FromQuery] string? author,
			[FromQuery] int? limit,
			[FromQuery] int? offset)
		{
			// TODO: Include ArticleFavorites, and Article Tags
			var query = context.Articles.AsNoTracking();

			var articles = await query
				.OrderByDescending(a => a.CreatedAt)
				.Skip(offset ?? 0)
				.Take(limit ?? 10)
				.Include(a => a.Publication).ThenInclude(p => p.PublicationImage)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.User)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.ProfilePicture)
				.Include(a => a.ArticleImage)
				.ToListAsync();

			List<ArticleCardModel> articleModels = mapper.Map<List<ArticleCardModel>>(articles);

			// TODO: Handle tags
			// TODO: Handle author

			return articleModels;
		}

		[HttpGet("@{author}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetIndependentArticle(string author, string slug)
		{
			/**
			 * FirstOrDefault() returns the first element or the default value (null for
			 * reference types). Meanwhile, First() throws an exception if no element is found.
			 */
			var article = await context.Articles.AsNoTracking()
				.Include(a => a.Tags)
				.Include(a => a.Publication)
				.Include(a => a.AuthorProfile)
				.ThenInclude(p => p.User)
				.FirstOrDefaultAsync(x => x.AuthorProfile.User.UserName == author && x.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			var profile = await _currentUserService.GetProfileAsync();

			var history = await context.ReadingHistories
				.FirstOrDefaultAsync(r => r.ProfileId == profile.ProfileId && r.ArticleId == article.Id);

			if (history == null)
			{
				history = new ReadingHistory
				{
					ProfileId = profile.ProfileId,
					ArticleId = article.Id,
					FirstReadAt = DateTime.UtcNow,
				};

				context.ReadingHistories.Add(history);
			}

			history.ReadCount++;
			history.LastReadAt = DateTime.UtcNow;

			await context.SaveChangesAsync();

			return Ok(mapper.Map<ArticleModel>(article));
		}

		[HttpGet("{publication}/{slug}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetPublicationArticle(string publication, string slug)
		{
			var article = await context.Articles.AsNoTracking()
				.Include(a => a.Publication)
				.Include(a => a.Tags)
				.FirstOrDefaultAsync(a =>
					a.Publication != null && a.Publication.Slug == publication
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

		[HttpGet("tag/{topic}")]
		public async Task<IActionResult> GetArticlesByTopic(string topic)
		{
			throw new NotImplementedException();
		}

		[Route("save")]
		[HttpPost]
		[Authorize] // TODO: Check permissions for posting articles
		public async Task<ArticleEditModel> Save(ArticleEditModel model)
		{
			model.AuthorProfileId = (await _currentUserService.GetProfileAsync()).ProfileId;

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
