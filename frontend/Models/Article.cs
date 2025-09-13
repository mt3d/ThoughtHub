namespace frontend.Models
{
	public class Article
	{
		public int ArticleId { get; set; }

		public string Slug { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public string BodySource { get; set; } = string.Empty;

		public List<CommentDto> Comments { get; set; } = new();

		public List<string> Tags { get; set; } = new();
		public DateTime CreateAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool Favorited { get; set; }
		public int FovoritesCount { get; set; }
		public string Image { get; set; } = string.Empty;

		public Author Author { get; set; } = new();
		public Publication Publication { get; set; } = new();

		public int ClapsCount { get; set; }
		public int CommentsCount { get; set; }
	}
}
