using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.RecommendedPublishers;
using ThoughtHub.Controllers;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Entities.Publications;

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

		public async Task<RecommendedPublisherConnectionModel> GetRecommendedPublishersAsync(int count, string? after, ClaimsPrincipal user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<UserPublisherModel>> SuggestProfiles(int profileId, int count = 3)
		{
			var profiles = await _context.Profiles.Take(count).ToListAsync();

			return _mapper.Map<IEnumerable<Data.Entities.Profile>, IEnumerable<UserPublisherModel>>(profiles);
		}

		public async Task<IEnumerable<PublicationPublisherModel>> SuggestPublications(int profileId, int count = 3)
		{
			var publications = await _context.Publications.Take(count).ToListAsync();

			return _mapper.Map<IEnumerable<Publication>, IEnumerable<PublicationPublisherModel>>(publications);
		}
	}
}
