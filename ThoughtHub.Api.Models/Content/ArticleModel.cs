namespace ThoughtHub.Api.Models.Content
{
	public class ArticleModel
	{
		public Guid Id { get; set; }

		// TODO: A slug based on the title.
		public string Slug { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;

		// TODO: We should use a permalink.
		public string Url { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public List<TagModel> Tags { get; set; } = new List<TagModel>();

		public IList<BlockModel> BlockModels { get; set; } = new List<BlockModel>();

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public int AuthorProfileId { get; set; }

		// TODO: Rename to ProfileModel and AutherProfile
		public AuthorModel Author { get; set; } = new();

		public int PublicationId { get; set; }

		public PublicationModel? Publication { get; set; } = new();

		// TODO: Add image.
		// TODO: Add comments.
		// TODO: Add claps.
	}
}
