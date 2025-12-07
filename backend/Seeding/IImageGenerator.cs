namespace ThoughtHub.Seeding
{
	public interface IImageGenerator
	{
		// TODO: Add the ability to specify the destination directory
		public void GenerateImages(int count, string? outputFolder = null, bool rewriteFiles = true);
	}
}
