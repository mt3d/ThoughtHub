namespace frontend.Models
{
	public class ArticlesWrapper
	{
		public List<Article> Articles { get; set; } = new();
		public int ArticlesCount { get; set; }
	}
}