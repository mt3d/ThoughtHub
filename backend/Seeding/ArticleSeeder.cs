using Bogus;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Seeding
{
	public class ArticleSeeder
	{
		private readonly PlatformContext _context;
		private readonly ImageCreator _imageCreator;

		public ArticleSeeder(PlatformContext context, ImageCreator imageCreator)
		{
			_context = context;
			_imageCreator = imageCreator;
		}

		public async Task SeedAsync(int count)
		{
			var articles = new List<Article>();
			var profiles = await _context.Profiles.ToListAsync();
			var tags = await _context.Tags.ToListAsync();
			var publications = await _context.Publications.ToListAsync();

			var faker = new Faker();

			for (int i = 0; i < count; i++)
			{
				var title = faker.Lorem.Sentence(faker.Random.Int(3, 8)).TrimEnd('.');
				var subtitle = faker.Lorem.Sentence(faker.Random.Int(8, 20)).TrimEnd('.');
				var slug = SeedingUtilities.Slugify(title);
				var author = faker.PickRandom(profiles);

				var article = new Article
				{
					Title = title,
					Description = subtitle,
					Slug = slug,
					AuthorProfile = author,
					Tags = faker.PickRandom(tags, faker.Random.Int(2, 5)).ToList(),
					ArticleImageId = await _imageCreator.CreateArticleImageAsync($"{title}_article_image.png", title)
				};

				// TODO: Should be between profile registration date and now
				article.CreatedAt = faker.Date.Past();
				if (faker.Random.Bool())
				{
					article.UpdatedAt = faker.Date.Between(article.CreatedAt, DateTime.Now);
				}
				else
				{
					article.UpdatedAt = article.CreatedAt;
				}

				if (faker.Random.Bool())
				{
					article.PublicationId = faker.PickRandom(publications).PublicationId;
				}

				_context.Articles.Add(article);
			}

			await _context.SaveChangesAsync();
		}
	}
}
