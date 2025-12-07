using Bogus;
using Microsoft.EntityFrameworkCore;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Services;

namespace ThoughtHub.Seeding
{
	public class ArticleSeeder
	{
		private readonly PlatformContext _context;
		private readonly IMediaService _mediaService;

		public ArticleSeeder(PlatformContext context, IMediaService mediaService)
		{
			_context = context;
			_mediaService = mediaService;
		}

		public async Task SeedAsync(int count)
		{
			var articles = new List<Article>();
			//
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
					ArticleImageId = await CreateArticleImageAsync($"{title}_article_image.png", title)
				};

				// TODO: Should be between profile registration date and now
				article.CreatedAt = faker.Date.Past();
				if (faker.Random.Bool())
				{
					article.UpdatedAt = faker.Date.Between(article.CreatedAt, DateTime.Now);
				}

				if (faker.Random.Bool())
				{
					article.PublicationId = faker.PickRandom(publications).PublicationId;
				}

				_context.Articles.Add(article);
			}

			await _context.SaveChangesAsync();
		}

		private async Task<Guid> CreateArticleImageAsync(string imageName, string title)
		{
			using var img = new Image<Rgba32>(800, 600);

			img.Mutate(ctx =>
			{
				// random gradient background
				var random = new Random();
				var c1 = new Rgba32((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
				var c2 = new Rgba32((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));

				ctx.Fill(Color.Transparent);
				ctx.Fill(new LinearGradientBrush(
					new PointF(0, 0),
					new PointF(800, 600),
					GradientRepetitionMode.Reflect,
					new[]
					{
				new ColorStop(0, c1),
				new ColorStop(1, c2)
					}));

				ctx.DrawText(title, SystemFonts.CreateFont("Arial", 60), Color.White, new PointF(20, 20));
			});

			using var stream = new MemoryStream();
			img.Save(stream, PngFormat.Instance);
			stream.Position = 0;

			var image = await _mediaService.AddAsync(imageName, stream);
			return image.Id;
		}
	}
}
