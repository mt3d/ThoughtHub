using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Claims;

namespace ThoughtHub.Controllers
{
	public class ArticleCreateDto
	{
		public List<string> TagList = new();
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

		// TODO: The models should be different.
		/// <summary>
		/// Get "For you" articles, which are articles recommended based on the user's reading history.
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		[HttpGet("/for-you")]
		[Authorize]
		public async Task<IList<ArticleCardModel>> GetForYou([FromQuery] int? limit, [FromQuery] int? offset)
		{
			// TODO: Find a better way to access current user profile.
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var readArticleIds = await context.ReadingHistories
				.Where(r => r.ProfileId == profile.ProfileId)
				.Select(a => a.ArticleId)
				.ToListAsync();

			//var recommendedArticles = await context.Articles
			//	.Include(a => a.AuthorProfile)
			//	.Include(a => a.Tags)
			//	.Where(a => a.Tags.Contains())

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
		public async Task<IList<ArticleCardModel>> GetFeatured([FromQuery] int? limit, [FromQuery] int? offset)
		{
			throw new NotImplementedException();
		}

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
				.Include(a => a.Tags)
				.Include(a => a.Publication)
				.Include(a => a.AuthorProfile)
				.ThenInclude(p => p.User)
				.FirstOrDefaultAsync(x => x.AuthorProfile.User.UserName == author && x.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			// TODO: find a better way to get profile.
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var history = await context.ReadingHistories
				.FirstOrDefaultAsync(r => r.ProfileId == profile.ProfileId && r.ArticleId == article.ArticleId);

			if (history == null)
			{
				history = new ReadingHistory
				{
					ProfileId = profile.ProfileId,
					ArticleId = article.ArticleId,
					FirstReadAt = DateTime.UtcNow,
				};

				context.ReadingHistories.Add(history);
			}

			history.ReadCount++;
			history.LastReadAt = DateTime.UtcNow;

			await context.SaveChangesAsync();

			return Ok(mapper.Map<ArticleModel>(article));
		}

		[HttpPost("/updateprogress")]
		[Authorize]
		public async Task<IActionResult> UpdateReadingProgress(int articleId, double progress, int readSeconds)
		{
			// TODO: Find a better way to access current user profile.
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var history = await context.ReadingHistories
				.FirstOrDefaultAsync(r => r.ProfileId == profile.ProfileId && r.ArticleId == articleId);

			if (history == null)
			{
				return NotFound();
			}

			history.Progress = Math.Max(history.Progress, progress);
			history.Completed = history.Progress >= 95;

			if (readSeconds > (history.TotalReadsSecond ?? 0)) // To avoid resets
			{
				history.TotalReadsSecond = readSeconds;
			}

			history.LastReadAt = DateTime.UtcNow;

			await context.SaveChangesAsync();
			return Ok();
		}

		[Authorize]
		[HttpGet("/recently_read")]
		public async Task<IActionResult> GetRecentlyRead(int limit = 5)
		{
			// TODO: Find a better way.
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var recentReads = await context.ReadingHistories
				.Include(r => r.Article)
				.ThenInclude(a => a.AuthorProfile)
				.Where(r => r.ProfileId == profile.ProfileId)
				.OrderByDescending(r => r.LastReadAt)
				.Take(limit)
				.ToListAsync();

			return Ok(mapper.Map<List<ArticleModel>>(recentReads));
		}

		[Authorize]
		[HttpGet("/continue_reading")]
		public async Task<IActionResult> GetContinueReading(int limit = 5)
		{
			// TODO: Find a better way.
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var recentReads = await context.ReadingHistories
				.Include(r => r.Article)
				.ThenInclude(a => a.AuthorProfile)
				.Where(r => r.ProfileId == profile.ProfileId && r.Progress <= 95)
				.OrderByDescending(r => r.LastReadAt)
				.Take(limit)
				.ToListAsync();

			return Ok(mapper.Map<List<ArticleModel>>(recentReads));
		}

		[HttpGet("/{publication}/{slug}")]
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

		[HttpGet("/tag/{topic}")]
		public async Task<IActionResult> GetArticlesByTopic(string topic)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(ArticleCreateDto model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var authorProfile = await context.Profiles.FirstAsync(p => p.UserId == userId);

			var tags = new List<Tag>();

			List<string> tagNames = model.TagList;
			List<string> normalizedNames = tagNames
				.Select(t => t.Trim().ToLowerInvariant())
				.Distinct()
				.ToList();

			// Naive implementation: loop over all tagNames. This is inefficient.
			// Better: Single query for existing tags.
			List<Tag> existingTags = await context.Tags
				.Where(t => normalizedNames.Contains(t.Name.ToLower()))
				.ToListAsync();

			List<Tag> newTags = normalizedNames
				.Except(existingTags.Select(t => t.Name.ToLowerInvariant()))
				.Select(name => new Tag { Name = name })
				.ToList();

			if (newTags.Any())
			{
				// Batch insert missing tags (more efficient).
				// TODO: might hit a race condition if two users try to add
				// the same new tag at the same time
				// Fix: Wrap in a try/catch for DbUpdateException on unique index
				// violation, then reload the conflicting tag from DB.
				await context.Tags.AddRangeAsync(newTags);
				await context.SaveChangesAsync(); // Generate Ids
			}

			List<Tag> allTags = existingTags.Concat(newTags).ToList();

			Article article = new();
			article.Tags = allTags;

			throw new NotImplementedException();
		}

		[HttpPut("{slug}")]
		[Authorize]
		public Task<IActionResult> Edit(string slug)
		{
			throw new NotImplementedException();
		}

		[HttpDelete("{slug}")]
		[Authorize]
		public Task<IActionResult> Delete(string slug)
		{
			throw new NotImplementedException();
		}

		[Route("Save")]
		[HttpPost]
		[Authorize] // TODO: Check permissions for posting articles
		public async Task<ArticleEditModel> Save(ArticleEditModel model)
		{
			if (string.IsNullOrEmpty(model.Published))
			{
				model.Published = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
			}

			var result = await Save(model, false);

			// TODO: Update all clients

			return result;
		}

		public class ArticleEditModel : ContentEditModel
		{
			// TODO: ImageField

			public string Slug { get; set; }

			public string MetaTitle { get; set; }

			public string OgTitle { get; set; }

			public string Published { get; set; }

			public int CommentCount { get; set; }

			// TODO: Add selected tags
			// TODO: Add tags


		}

		public abstract class ContentEditModel
		{
			public Guid Id { get; set; }

			public string Title { get; set; }
		}

		private async Task<ArticleEditModel> Save(ArticleEditModel model, bool draft = false)
		{
			try
			{

			}
			catch
			{

			}

			throw new NotImplementedException();
		}
	}
}
