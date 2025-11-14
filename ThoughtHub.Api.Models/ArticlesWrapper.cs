namespace ThoughtHub.Api.Models
{
	public class ArticlesWrapper
	{
		public List<ArticleModel> Articles { get; set; } = new();
		public int Count { get; set; }
	}
}
