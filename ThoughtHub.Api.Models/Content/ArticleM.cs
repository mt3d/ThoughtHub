namespace ThoughtHub.Api.Models.Content
{
	public class ArticleM
	{
		public Guid Id { get; set; }

		public List<TagM> Tags { get; set; } = new List<TagM>();

		public string Slug { get; set; }

		public string Title { get; set; }

		public IList<BlockModel> BlockModels { get; set; } = new List<BlockModel>();

		public int AuthorProfileId { get; set; }
	}

	public class TagM
	{
	}
}
