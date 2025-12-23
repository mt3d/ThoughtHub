using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.Article;

namespace ThoughtHub.Data.Entities
{
	public class ReadingHistory
	{
		public int ReadingHistoryId { get; set; }

		public int ProfileId { get; set; }

		public Profile Profile { get; set; }

		public Guid ArticleId { get; set; }

		public Article Article { get; set; }

		public DateTime FirstReadAt { get; set; }

		public DateTime LastReadAt { get; set; }

		public int ReadCount { get; set; } = 1;

		public double Progress { get; set; }

		public int? TotalReadsSecond { get; set; }

		public bool Completed { get; set; } = false;

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ReadingHistory>(builder =>
			{
				builder.HasIndex(r => new { r.ProfileId, r.ArticleId })
				.IsUnique();

			builder.HasOne(r => r.Profile)
				.WithMany()
				.HasForeignKey(r => r.ProfileId)
				.OnDelete(DeleteBehavior.Restrict); // TODO: Explain

			builder.HasOne(r => r.Article)
				.WithMany()
				.HasForeignKey(r => r.ArticleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(r => r.Progress)
				.HasPrecision(5, 2); // TODO: Explain
			});
		}
	}
}
