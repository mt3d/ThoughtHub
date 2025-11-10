namespace ThoughtHub.Data.Entities
{
	public class Tag
	{
		public int TagId { get; set; }

		public string Name { get; set; }

		public List<Article> Articles { get; set; } = new();
	}
}
