using Bogus;
using System.Text.RegularExpressions;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Seeding
{
	public class PublicationSeeder
	{
		private readonly PlatformContext _context;

		public PublicationSeeder(PlatformContext context)
		{
			_context = context;
		}

		public async Task SeedAsync(int count)
		{
			var faker = new Faker();
			var publications = new List<Publication>();

			for (int i = 0; i < count; i++)
			{
				var name = faker.Company.CatchPhrase();
				var slug = Slugify(name);

				publications.Add(new Publication
				{
					Name = name,
					Slug = slug
				});
			}

			await _context.Publications.AddRangeAsync(publications);
			await _context.SaveChangesAsync();
		}

		private string Slugify(string name)
		{
			name = name.ToLowerInvariant();

			// Remove invalid characters
			// ^ inside [] negates it, meaning match any character not listed.
			// \s → any whitespace (space, tab, etc.)
			name = Regex.Replace(name, @"[^a-z0-9\s-]", "");

			// Replace spaces with dash
			name = Regex.Replace(name, @"\s+", "-");

			// Collapse multiple dashes
			name = Regex.Replace(name, @"-+", "-");

			return name;
		}
	}
}
