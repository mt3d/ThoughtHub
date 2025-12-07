using Jdenticon;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ThoughtHub.Services;

namespace ThoughtHub.Seeding
{
	public class ImageCreator
	{
		private readonly IMediaService _mediaService;

		public ImageCreator(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public async Task<Guid> CreateProfileImageAsync(string name)
		{
			using var stream = new MemoryStream();

			var seed = Guid.NewGuid().ToString();
			var icon = Identicon.FromValue(seed, 256);
			icon.SaveAsPng(stream);
			stream.Position = 0;

			var image = await _mediaService.AddAsync(name, stream);
			return image.Id;
		}

		public async Task<Guid> CreateArticleImageAsync(string imageName, string title)
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
