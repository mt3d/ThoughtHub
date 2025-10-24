using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Data
{
	public class PlatformContext : IdentityDbContext<User>
	{
		public PlatformContext(DbContextOptions<PlatformContext> options) : base(options) { }

		public DbSet<Article> Articles => Set<Article>();
		public DbSet<Tag> Tags => Set<Tag>();
		public DbSet<Comment> Comments => Set<Comment>();

		public DbSet<Profile> Profiles => Set<Profile>();
		public DbSet<FollowMapping> FollowMappings => Set<FollowMapping>();

		public DbSet<Publication> Publications => Set<Publication>();
		public DbSet<PublicationFollower> PublicationFollowers => Set<PublicationFollower>();
		public DbSet<PublicationMember> PublicationMembers => Set<PublicationMember>();

		public DbSet<ReadingHistory> ReadingHistories => Set<ReadingHistory>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// This line is critical! It sets up keys for Identity tables.
			base.OnModelCreating(builder);

			/*
			* Composite priamry key. The private key is a combination of the two Ids.
			* One User can follow another User only once (no duplication).
			* 
			* By using the pivot table, we're converting the many-to-many relationship
			* into two one-to-many realtionships.
			*/
			builder.Entity<FollowMapping>().HasKey(k => new { k.FollowerId, k.FolloweeId });

			/* The first relationship.
				*
				* A one-to-many relationship is made up from:
				* One primary on the principal entity (the "one" end of the relationship).
				* One or more foreign key properties on the dependent entity; that is the
				* "many" end of the relationship.
				* 
				* In the case of many-to-many relationships, the foreign keys are moved
				* to the pivot table.
				*/
			builder.Entity<FollowMapping>()
				.HasOne(table => table.Followee)
				.WithMany(user => user.Followers)
				.HasForeignKey(table => table.FolloweeId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<FollowMapping>()
				.HasOne(table => table.Follower)
				.WithMany(user => user.Followees)
				.HasForeignKey(table => table.FollowerId)
				.OnDelete(DeleteBehavior.Restrict);

			// Configure publications

			builder.Entity<Publication>()
				.HasMany(p => p.Articles)
				.WithOne(a => a.Publication)
				.HasForeignKey(a => a.PublicationId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Publication>()
				.HasIndex(p => p.Slug)
				.IsUnique();

			builder.Entity<Publication>()
				.HasOne(p => p.Owner)
				.WithMany()
				.HasForeignKey(p => p.OwnerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Profile>()
				.HasMany(p => p.FollowedPublications)
				.WithMany(p => p.Followers)
				.UsingEntity<PublicationFollower>();

			builder.Entity<Profile>()
				.HasMany(p => p.MemberPublications)
				.WithMany(p => p.Members)
				.UsingEntity<PublicationMember>();

			builder.Entity<ReadingHistory>()
				.HasIndex(r => new { r.ProfileId, r.ArticleId })
				.IsUnique();

			builder.Entity<ReadingHistory>()
				.HasOne(r => r.Profile)
				.WithMany()
				.HasForeignKey(r => r.ProfileId)
				.OnDelete(DeleteBehavior.Restrict); // TODO: Explain

			builder.Entity<ReadingHistory>()
				.HasOne(r => r.Article)
				.WithMany()
				.HasForeignKey(r => r.ArticleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<ReadingHistory>()
				.Property(r => r.Progress)
				.HasPrecision(5, 2); // TODO: Explain

			builder.Entity<Tag>()
				.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.Entity<Tag>()
				.HasIndex(t => t.Name)
				.IsUnique();

			builder.Entity<Tag>()
				.Property(t => t.Name)
				.UseCollation("SQL_Latin1_General_CP1_CI_AS"); // TODO: Explain. Use case-insensitive comparisons.
		}

		// TODO: Handle transaction
	}
}
