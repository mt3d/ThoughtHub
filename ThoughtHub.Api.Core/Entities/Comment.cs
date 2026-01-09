using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ThoughtHub.Api.Core.Entities.Article;

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
		public Comment? ParentComment { get; set; }

		/// <summary>
		/// The id of the comment's author.
		/// </summary>
		public Guid AuthorId { get; set; }

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
		public Guid ArticleId { get; set; }

		/// <summary>
		/// The parent article.
		/// </summary>
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
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Mark the comment as pinned. Authors/Publications can pin comments.
		/// </summary>
		public bool IsPinned { get; set; }

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Comment>(options =>
			{
				options.HasOne(c => c.Author)
					.WithMany()
					.HasForeignKey(c => c.AuthorId)
					.OnDelete(DeleteBehavior.NoAction);
			});
		}
	}
}
