namespace ThoughtHub.Data.Entities.Media
{
	/// <summary>
	/// Represents a physical or historical variant of that logical media: for
	/// example, the same image cropped, resized, or replaced with an updated file,
	/// perhaps to fix colors or reframe a portrait. In other words, MediaVersion
	/// is a snapshot of the actual stored file (binary data and its attributes).
	/// </summary>
	public class ImageVersion
	{
		public int Id { get; set; }

		public long Size { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string FileExtension { get; set; }

		public int BaseImageId { get; set; }

		public Image BaseImage { get; set; }
	}
}
