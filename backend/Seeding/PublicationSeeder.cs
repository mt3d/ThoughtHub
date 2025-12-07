using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Seeding
{
	public class PublicationSeeder
	{
		private readonly PlatformContext _context;
		private readonly ImageCreator _imageCreator;

		public PublicationSeeder(PlatformContext context, ImageCreator imageCreator)
		{
			_context = context;
			_imageCreator = imageCreator;
		}

		public async Task SeedAsync(int count)
		{
			var faker = new Faker();
			var profiles = await _context.Profiles.ToListAsync();
			var publications = new List<Publication>();

			for (int i = 0; i < count; i++)
			{
				var name = faker.Company.CatchPhrase();
				var slug = SeedingUtilities.Slugify(name);

				publications.Add(new Publication
				{
					Name = name,
					Slug = slug,
					OwnerId = faker.PickRandom(profiles).ProfileId,
					CreatedAt = faker.Date.Past(),
					PublicationImageId = await _imageCreator.CreatePublicationImageAsync($"{slug}_publication_image.png")
					// TODO: Add random members, followers, description, tagline, and update time
				});
			}

			await _context.Publications.AddRangeAsync(publications);
			await _context.SaveChangesAsync();
		}
	}
}
