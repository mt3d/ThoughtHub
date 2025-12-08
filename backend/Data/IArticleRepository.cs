namespace ThoughtHub.Data
{
	public interface IArticleRepository
	{
		public Task Save(ArticleM model);
	}
}
