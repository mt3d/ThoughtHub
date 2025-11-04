namespace ThoughtHub.Data.Entities.Media
{
	public class ImageFolder
	{
		public int Id { get; set; }

		public int ParentFolderId { get; set; }

		public ImageFolder Parent { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime CreatedAt { get; set; }

		public IList<Image> Images { get; set; }
	}
}
