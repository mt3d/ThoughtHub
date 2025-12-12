namespace ThoughtHub.Api.Core.Entities.Article
{
	public sealed class Block
	{
		public Guid Id { get; set; }

		public string ClrType { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public IList<BlockField> Fields { get; set; } = new List<BlockField>();
	}
}
