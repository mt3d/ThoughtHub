namespace ThoughtHub.Data
{
	public class ArticleM
	{
		public Guid Id { get; set; }

		public List<TagM> Tags { get; set; }

		public string Slug { get; set; }

		public string Title { get; set; }
	}

	public class TagM
	{
	}
}
