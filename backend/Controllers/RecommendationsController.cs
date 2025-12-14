using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Api.Models;
using ThoughtHub.Data;
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

		public RecommendationsController(
			PlatformContext context,
			IMapper mapper,
			ICurrentUserService currentUserService)
		{
			_context = context;
			_mapper = mapper;
			_currentUserService = currentUserService;
		}

		/// <summary>
		/// Get "For you" articles, which are articles recommended based on the user's reading history.
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		[HttpGet("for-you")]
		[Authorize]
		public async Task<IList<ArticleCardModel>> GetForYou([FromQuery] int? limit, [FromQuery] int? offset)
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
		public async Task<IList<ArticleCardModel>> GetFeatured([FromQuery] int? limit, [FromQuery] int? offset)
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

			return Ok(_mapper.Map<List<ArticleCardModel>>(recentReads));
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

			return Ok(_mapper.Map<List<ArticleCardModel>>(recentReads));
		}
	}
}
