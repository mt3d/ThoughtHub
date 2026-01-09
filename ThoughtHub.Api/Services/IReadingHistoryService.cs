namespace ThoughtHub.Services
{
	public interface IReadingHistoryService
	{
		Task UpdateArticleHistory(Guid articleId, Guid profileId);
	}
}
