namespace ThoughtHub.Api.Core.Entities.Article
{
	public class ArticleRevision
	{
		public Guid Id { get; set; }

		/// <summary>
		/// The data of the revision serialized as JSON.
		/// </summary>
		public string Data { get; set; }

		public DateTime CreatedAt { get; set; }

		public Guid ArticleId { get; set; }

		public Article Article { get; set; }

		// TODO: Add a method that deserializes the data.
	}
}
