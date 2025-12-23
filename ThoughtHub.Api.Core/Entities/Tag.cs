using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.Article;

namespace ThoughtHub.Data.Entities
{
	public class Tag
	{
		public int TagId { get; set; }

		public string Name { get; set; }

		public List<Article> Articles { get; set; } = new();

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Tag>(builder =>
			{
				builder.Property(t => t.Name)
					.IsRequired()
					.HasMaxLength(50);

				builder.HasIndex(t => t.Name)
					.IsUnique();

				builder.Property(t => t.Name)
					.UseCollation("SQL_Latin1_General_CP1_CI_AS"); // TODO: Explain. Use case-insensitive comparisons.
			});
		}
	}
}
