namespace ThoughtHub.Data.Entities.Media
{
	/// <summary>
	/// Represents a logical piece of content: “this is the cover image of article X”
	/// or “this is Mark’s profile photo”. It’s a stable identity, something you can
	/// reference consistently over time.
	/// </summary>
	public class Image
	{
		public Guid Id { get; set; }

		// TODO: for which version?
		// TODO: With extension?
		public string Filename { get; set; } = string.Empty;

		public string ContentType { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;

		public string AltText { get; set; } = string.Empty;

		public long Size { get; set; }

		// TODO: For which version?
		public string PublicUrl { get; set; } = string.Empty;

		// TODO: Might be removed
		public int Width { get; set; }

		// TODO: Might be removed
		public int Height { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public int? ImageFolderId { get; set; }

		public ImageFolder? Folder { get; set; }

		public IList<ImageVersion> Versions { get; set; } = new List<ImageVersion>();
	}
}
