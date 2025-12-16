using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using ThoughtHub.Api.Models;
using ThoughtHub.Data;
using ThoughtHub.Recommendation;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	[Route("api/recommend")]
	[ApiController]
	public class RecommendationsController : Controller
	{
		private readonly PlatformContext _context;
		private readonly IMapper _mapper;
		private readonly ICurrentUserService _currentUserService;
		private readonly IRecommendationService _recommendationService;

		public RecommendationsController(
			PlatformContext context,
			IMapper mapper,
			ICurrentUserService currentUserService,
			IRecommendationService recommendationService)
		{
			_context = context;
			_mapper = mapper;
			_currentUserService = currentUserService;
			_recommendationService = recommendationService;
		}

		/// <summary>
		/// Get "For you" articles, which are articles recommended based on the user's reading history.
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		[HttpGet("for-you")]
		[Authorize]
		public async Task<IList<ArticleDigestModel>> GetForYou([FromQuery] int? limit, [FromQuery] int? offset)
		{
			var profile = await _currentUserService.GetProfileAsync();

			var readArticleIds = await _context.ReadingHistories
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
		[HttpGet("featured")]
		[Authorize]
		public async Task<IList<ArticleDigestModel>> GetFeatured([FromQuery] int? limit, [FromQuery] int? offset)
		{
			throw new NotImplementedException();
		}

		[Authorize]
		[HttpGet("recently_read")]
		public async Task<IActionResult> GetRecentlyRead(int limit = 5)
		{
			var profile = await _currentUserService.GetProfileAsync();

			var recentReads = await _context.ReadingHistories
				.Include(r => r.Article)
				.ThenInclude(a => a.AuthorProfile)
				.Where(r => r.ProfileId == profile.ProfileId)
				.OrderByDescending(r => r.LastReadAt)
				.Take(limit)
				.ToListAsync();

			return Ok(_mapper.Map<List<ArticleDigestModel>>(recentReads));
		}

		[Authorize]
		[HttpGet("continue_reading")]
		public async Task<IActionResult> GetContinueReading(int limit = 5)
		{
			var profile = await _currentUserService.GetProfileAsync();

			var recentReads = await _context.ReadingHistories
				.Include(r => r.Article)
				.ThenInclude(a => a.AuthorProfile)
				.Where(r => r.ProfileId == profile.ProfileId && r.Progress <= 95)
				.OrderByDescending(r => r.LastReadAt)
				.Take(limit)
				.ToListAsync();

			return Ok(_mapper.Map<List<ArticleDigestModel>>(recentReads));
		}

		/// <summary>
		/// Returns a slice of unpersonalized feed.
		/// </summary>
		/// <returns></returns>
		[HttpGet("recent_unpersonalized")]
		public async Task<IList<ArticleDigestModel>> GetRecentArticles(
			[FromQuery] int limit,
			[FromQuery] int offset = 0) // TODO: Add filtering by tag and author
		{
			var query = _context.Articles.AsNoTracking();

			var articles = await query
				.OrderByDescending(a => a.CreatedAt)
				.Skip(offset)
				.Take(limit)
				.Include(a => a.Publication).ThenInclude(p => p.PublicationImage)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.User)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.ProfilePicture)
				.Include(a => a.ArticleImage)
				.ToListAsync();

			List<ArticleDigestModel> articleModels = _mapper.Map<List<ArticleDigestModel>>(articles);

			// TODO: Handle tags
			// TODO: Handle author

			return articleModels;
		}

		[HttpGet("topics")]
		public async Task<IActionResult> GetTopics([FromQuery] int count = 3)
		{
			var profile = await _currentUserService.GetProfileAsync();
			var result = await _recommendationService.SuggestTopics(profile.ProfileId, count);

			return Ok(result);
		}
	}
}
