namespace ThoughtHub.Seeding
{
	public class ImageSharpImageGenerator : IImageGenerator
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

			}
		}
	}
}
