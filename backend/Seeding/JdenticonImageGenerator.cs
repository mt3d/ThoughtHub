using Jdenticon;

namespace ThoughtHub.Seeding
{
	public class JdenticonImageGenerator : IImageGenerator
	{
		public void GenerateImages(int count, string? outputFolder = null, bool rewriteFiles = true)
		{
			if (outputFolder is null)
			{
				outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "ProfileImages");
			}

			Directory.CreateDirectory(outputFolder);

			for (int i = 0; i < count; i++)
			{
				var seed = Guid.NewGuid().ToString();
				var icon = Identicon.FromValue(seed, 256);

				var filepath = Path.Combine(outputFolder, $"profile_{i + 1}.png");

				if (rewriteFiles && File.Exists(filepath))
				{
					continue;
				}

				icon.SaveAsPng(filepath);
			}
		}
	}
}
