namespace ThoughtHub.Api.Models
{
	public class CommentModel
	{
		public int CommentId { get; set; }

		public int? ParentCommentId { get; set; }

		public CommentModel? ParentComment { get; set; }

		public List<CommentModel> Replies { get; set; } = new();

		public AuthorModel? Author { get; set; }

		public string? BodyHtml { get; set; } = string.Empty;

		public string? BodySource { get; set; } = string.Empty;

		public int ClapsCount { get; set; }

		public int RepliesCount { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public bool IsEdited { get; set; }

		public bool IsDeleted { get; set; }

		public bool IsPinned { get; set; }
	}
}
