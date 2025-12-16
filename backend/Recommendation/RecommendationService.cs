using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Models;
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

	public class RecommendationService : IRecommendationService
	{
		private readonly PlatformContext _context;
		private readonly IMapper _mapper;

		public RecommendationService(
			PlatformContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
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

		public async Task<IEnumerable<TagModel>> SuggestTopics(int profileId, int count = 3)
		{
			var topics = await _context.Tags.ToListAsync();

			return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagModel>>(topics.Take(count));
		}
	}
}
