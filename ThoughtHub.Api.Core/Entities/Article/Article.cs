using System.Text.Json.Serialization;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Api.Core.Entities.Article
{
	public class Article
	{
		public Guid Id { get; set; }

		public string? Slug { get; set; }

		public string? Title { get; set; }

		/// <summary>
		/// Subtitle of the article.
		/// </summary>
		public string? Description { get; set; }

		public IList<ArticleBlock> Blocks { get; set; } = new List<ArticleBlock>();

		public Guid? AuthorProfileId { get; set; }

		public Profile? AuthorProfile { get; set; }

		public List<Comment> Comments { get; set; } = new();

		public int FovoritesCount { get; set; } // Dynamically generated

		public Image? ArticleImage { get; set; }

		public Guid? ArticleImageId { get; set; }

		public int ClapsCount { get; set; } // Dynamically generated

		public int CommentsCount { get; set; } // Dynamically generated

		public int? PublicationId { get; set; }

		[JsonIgnore]
		public Publication? Publication { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// Gets or sets the list of tags applied to this article.
		/// </summary>
		public List<Tag> Tags { get; set; } = new();
	}
}
