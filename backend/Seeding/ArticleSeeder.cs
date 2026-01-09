using Bogus;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Blocks;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data;
using ThoughtHub.Services;

namespace ThoughtHub.Seeding
{
	public class ArticleSeeder
	{
		private readonly PlatformContext _context;
		private readonly ImageCreator _imageCreator;
		private readonly IArticleService _articleService;

		public ArticleSeeder(
			PlatformContext context,
			ImageCreator imageCreator,
			IArticleService articleService)
		{
			_context = context;
			_imageCreator = imageCreator;
			_articleService = articleService;
		}

		public async Task SeedAsync(int count)
		{
			//var articles = new List<Article>();
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

				var article = new ArticleModel
				{
					Title = title,
					Description = subtitle,
					Slug = slug,
					AuthorProfileId = author.Id,
					// TODO: Handle tags.
					// TODO: Handle images.
					// ArticleImageId = await _imageCreator.CreateArticleImageAsync($"{title}_article_image.png", title)
				};

				var blocksCount = faker.PickRandom(Enumerable.Range(5, 15));
				for (int j = 0; j < blocksCount; j++)
				{
					article.BlockModels.Add(new TextBlockModel
					{
						Body = faker.Lorem.Paragraph()
					});
				}

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

				await _articleService.SaveAsync(article);
			}
		}
	}
}
