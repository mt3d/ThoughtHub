using ThoughtHub.Data.Identity;
using System.Text.Json.Serialization;

namespace ThoughtHub.Data.Entities
{
	public class Comment
	{
		/// <summary>
		/// A unique comment id.
		/// </summary>
		public int CommentId { get; set; }

		/// <summary>
		/// The id of the parent comment. If null, then the comment is a top-level one.
		/// </summary>
		public int? ParentCommentId { get; set; }

		/// <summary>
		/// The parent comment.
		/// </summary>
		[JsonIgnore]
		public Comment? ParentComment { get; set; }

		/// <summary>
		/// The id of the comment's author.
		/// </summary>
		[JsonIgnore]
		public int AuthorId { get; set; }

		/// <summary>
		/// The comment's author.
		/// </summary>
		public Profile Author { get; set; }

		/// <summary>
		/// A rendered version of the comment.
		/// </summary>
		public string BodyHtml { get; set; } = string.Empty;

		/// <summary>
		/// The raw source (markdown) of the comment.
		/// </summary>
		public string BodySource { get; set; } = string.Empty;

		/// <summary>
		/// The number of claps for the comment.
		/// </summary>
		public int ClapsCount { get; set; } // Store to avoid recalculation.

		/// <summary>
		/// The number of replies for the comment.
		/// </summary>
		public int RepliesCount { get; set; }

		/// <summary>
		/// The ID of the parent article, stored for efficient filtering.
		/// No need to include any information about the article when returning comments.
		/// </summary>
		[JsonIgnore]
		public int ArticleId { get; set; }

		/// <summary>
		/// The parent article.
		/// </summary>
		[JsonIgnore]
		public Article? Article { get; set; }

		/// <summary>
		/// Creation date of the comment.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Last update date of the comment.
		/// </summary>
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// Indicates whether the comment was updated once or more.
		/// </summary>
		public bool IsEdited { get; set; }

		/// <summary>
		/// Mark the comment as deleted. Delted comments are hidden but preserved
		/// for moderation purposes.
		/// </summary>
		[JsonIgnore]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Mark the comment as pinned. Authors/Publications can pin comments.
		/// </summary>
		[JsonIgnore]
		public bool IsPinned { get; set; }
	}
}
