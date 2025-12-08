using System.Text.Json.Serialization;

namespace ThoughtHub.Api.Core.Entities.Article
{
	public class ArticleBlock
	{
		public Guid Id { get; set; }

		public Guid ArticleId { get; set; }

		public Guid BlockId { get; set; }

		public int SortOrder { get; set; }

		[JsonIgnore]
		public Article Article { get; set; }

		[JsonIgnore]
		public Block Block { get; set; }
	}
}
