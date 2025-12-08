using Microsoft.EntityFrameworkCore;
using ThoughtHub.Controllers;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Recommendation
{
	public class InterestProfile
	{
		public Tag Tag { get; set; }
		public int Weight { get; set; }
	}

	public class RecommendationService
	{
		private readonly PlatformContext _context;

		public RecommendationService(PlatformContext context)
		{
			_context = context;
		}

		public async Task<IList<InterestProfile>> GenerateInterestProfile(int profileId)
		{
			var profileTagWeights = await _context.ReadingHistories
				.Where(r => r.ProfileId == profileId && r.Progress >= 50)
				.Include(r => r.Article)
				.ThenInclude(a => a.Tags)
				/**
				 * Originally: r => r.Article.Tags returns IList<Tag>
				 * 
				 * Select many flattens queries that return lists of lists.
				 * But we will get repated tags.
				 */
				.SelectMany(r => r.Article.Tags)
				.GroupBy(tag => tag)
				.Select(group => new InterestProfile
				{
					Tag = group.Key,
					Weight = group.Count()
				})
				.OrderByDescending(x => x.Weight)
				.ToListAsync();

			return profileTagWeights;
		}

	}
}
