namespace ThoughtHub.Data.Entities
{
	public class ReadingHistory
	{
		public int ReadingHistoryId { get; set; }

		public int ProfileId { get; set; }

		public Profile Profile { get; set; }

		public int ArticleId { get; set; }

		public Article Article { get; set; }

		public DateTime FirstReadAt { get; set; }

		public DateTime LastReadAt { get; set; }

		public int ReadCount { get; set; } = 1;

		public double Progress { get; set; }

		public int? TotalReadsSecond { get; set; }

		public bool Completed { get; set; } = false;
	}
}
