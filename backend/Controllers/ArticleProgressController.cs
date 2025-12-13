using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Data;

namespace ThoughtHub.Controllers
{
	public class ArticleProgressController : Controller
	{
		private readonly PlatformContext _context;

		private async Task<Data.Entities.Profile> GetCurrentUserProfileAsync()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = await _context.Profiles.FirstAsync(p => p.UserId == userId);

			return profile;
		}

		public ArticleProgressController(PlatformContext context)
		{
			_context = context;
		}

		[HttpPost("updateprogress")]
		[Authorize]
		public async Task<IActionResult> UpdateReadingProgress(Guid articleId, double progress, int readSeconds)
		{
			var profile = await GetCurrentUserProfileAsync();

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

			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
