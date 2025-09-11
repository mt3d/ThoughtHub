using System.Text.Json.Serialization;

namespace backend.Data.Entities
{
	public class Comment
	{
		public int CommentId { get; set; }

		public string? ParentCommentId { get; set; }
		public Comment? ParentComment { get; set; }

		[JsonIgnore]
		public int AuthorId { get; set; }
		public User? Author { get; set; }

		public string BodyHtml { get; set; } = string.Empty; // A rendered version of the comment
		public string BodySource { get; set; } = string.Empty; // Markdown (raw input)

		// Store to avoid recalculating
		public int ClapsCount { get; set; }
		public int RepliesCount { get; set; }

		// The parent article.
		// Store the article ID for efficient for filtering. No need to include the article.
		// When returning a comment,
		// do not return the full article.
		[JsonIgnore]
		public int ArticleId { get; set; }
		[JsonIgnore]
		public Article? Article { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsEdited { get; set; }

		// Delted comments are hidden but preserved (for moderation purposes)
		public bool IsDeleted { get; set; }

		// Authors/Publications can pin comments
		public bool IsPinned { get; set; }
	}
}
