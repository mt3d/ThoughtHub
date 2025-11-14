namespace ThoughtHub.Api.Models
{
	public class ArticleCardModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public DateTime CreateAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public int FavoritesCount { get; set; }

		public string Image { get; set; } = string.Empty;

		public AuthorModel Author { get; set; } = new();

		public PublicationModel? Publication { get; set; } = new();

		public int ClapsCount { get; set; }

		public int CommentsCount { get; set; }
	}
}
