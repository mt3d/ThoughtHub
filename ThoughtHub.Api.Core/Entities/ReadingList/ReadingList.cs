using Microsoft.EntityFrameworkCore;

namespace ThoughtHub.Api.Core.Entities.ReadingList
{
	public sealed class ReadingList
	{
		public Guid Id { get; set; }

		public Guid OwnerId { get; set; }

		public string Name { get; set; } = string.Empty;

		public string? Description { get; set; }

		public string Slug { get; set; } = string.Empty;

		public ReadingListVisibility Visibility { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public List<ReadingListArticle> _items = [];

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ReadingList>(builder =>
			{

				builder.HasKey(l => l.Id);

				// Fast slug lookup.
				builder.HasIndex(l => new { l.OwnerId, l.Slug });

				builder.Property(l => l.Slug)
					.HasMaxLength(200)
					.IsRequired();

				builder.Property(l => l.Name)
					.HasMaxLength(200)
					.IsRequired();
			});
		}
	}
}
