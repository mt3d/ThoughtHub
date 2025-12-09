namespace ThoughtHub.Api.Models
{
	public class ArticleModel
	{
		public Guid Id { get; set; }

		// TODO: This is never referenced. Remove.
		public string Slug { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string BodySource { get; set; } = string.Empty;

		//public List<CommentModel> Comments { get; set; } = new();

		public List<TagModel> Tags { get; set; } = new();

		public DateTime CreateAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		// TODO: Should be removed. No longer used in medium.com.
		public bool Favorited { get; set; }

		public int FavoritesCount { get; set; }

		// TODO: Handle properly.
		public string Image { get; set; } = string.Empty;

		public AuthorModel Author { get; set; } = new();
		
		public PublicationModel? Publication { get; set; } = new();

		public int ClapsCount { get; set; }

		public int CommentsCount { get; set; }
	}
}
