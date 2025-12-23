using Microsoft.EntityFrameworkCore;

namespace ThoughtHub.Api.Core.Entities.ReadingList
{
	public sealed class ReadingList
	{
		public Guid Id { get; private set; }

		public Guid OwnerId { get; private set; }

		public ReadingListType Type { get; private set; }

		public string Name { get; private set; } = string.Empty;

		public string? Description { get; private set; }

		public string Slug { get; private set; } = string.Empty;

		public ReadingListVisibility Visibility { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		private List<ReadingListArticle> _items = [];
		public IReadOnlyCollection<ReadingListArticle> Items => _items;

		private ReadingList(Guid ownerId, ReadingListType type, string name, string slug, ReadingListVisibility visibility)
		{
			Id = Guid.NewGuid();
			OwnerId = ownerId;
			Name = name;
			Slug = slug;
			Type = type;
			Visibility = visibility;
			CreatedAt = DateTime.UtcNow;
		}

		public static ReadingList CreateDefault(Guid ownerId)
		{
			return new ReadingList(ownerId, ReadingListType.System, "Reading List", "reading-list", ReadingListVisibility.Private);
		}

		public static ReadingList CreateCustom(Guid ownerId, string name, string slug)
		{
			return new ReadingList(ownerId, ReadingListType.Custom, name, slug, ReadingListVisibility.Private);
		}

		public void UpdateMetadata(string name, string? description)
		{
			if (Type == ReadingListType.System)
			{
				throw new InvalidOperationException("System reading lists metadata cannot be modified");
			}

			Name = name;
			Description = description;
		}

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ReadingList>(builder =>
			{

				builder.HasKey(l => l.Id);

				// Fast slug lookup.
				builder.HasIndex(l => new { l.OwnerId, l.Slug })
					.IsUnique();

				// WARNING: Only for SQL Server
				builder.HasIndex(x => new { x.OwnerId, x.Type })
				   .IsUnique()
				   .HasFilter($"[{nameof(ReadingList.Type)}] = {(int)ReadingListType.System}");

				builder.Property(l => l.Type)
					.IsRequired();

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
