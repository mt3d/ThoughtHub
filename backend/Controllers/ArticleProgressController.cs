using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Data;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	public class ArticleProgressController : Controller
	{
		private readonly PlatformContext _context;
		private readonly ICurrentUserService _currentUserService;

		public ArticleProgressController(
			PlatformContext context,
			ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}

		[HttpPost("updateprogress")]
		[Authorize]
		public async Task<IActionResult> UpdateReadingProgress(Guid articleId, double progress, int readSeconds)
		{
			var profile = await _currentUserService.GetProfileAsync();

			var history = await _context.ReadingHistories
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

			await _context.SaveChangesAsync();
			return Ok();
		}

	}
}
