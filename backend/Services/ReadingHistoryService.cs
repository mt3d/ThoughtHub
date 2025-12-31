using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Services
{
	public class ReadingHistoryService : IReadingHistoryService
	{
		private readonly PlatformContext _context;

		public ReadingHistoryService(PlatformContext context)
		{
			_context = context;
		}

		public async Task UpdateArticleHistory(Guid articleId, Guid profileId)
		{
			var history = await _context.ReadingHistories
				.FirstOrDefaultAsync(r => r.ProfileId == profileId && r.ArticleId == articleId);

			if (history is null)
			{
				history = new ReadingHistory
				{
					ProfileId = profileId,
					ArticleId = articleId,
					FirstReadAt = DateTime.UtcNow,
				};

				_context.ReadingHistories.Add(history);
			}

			history.ReadCount++;
			history.LastReadAt = DateTime.UtcNow;

			await _context.SaveChangesAsync();
		}
	}
}
