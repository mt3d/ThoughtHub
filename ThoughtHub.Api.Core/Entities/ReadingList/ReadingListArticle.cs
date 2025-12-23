namespace ThoughtHub.Api.Core.Entities.ReadingList
{
	public sealed class ReadingListArticle
	{
		public Guid Id { get; set; }

		public Guid ReadingListId { get; set; }

		public Guid ArticleId { get; set; }

		public DateTime CreatedAt { get; set; }

		public ReadingListArticle(Guid readingListId, Guid articleId)
		{
			Id = Guid.NewGuid();
			ReadingListId = readingListId;
			ArticleId = articleId;
			CreatedAt = DateTime.UtcNow;
		}
	}
}
