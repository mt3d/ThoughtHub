using Microsoft.EntityFrameworkCore;
using backend.Data.Entities;
using backend.Data.Identity;

namespace backend.Data
{
	public class PlatformContext(DbContextOptions<PlatformContext> options) : DbContext(options)
	{
		public DbSet<Article> Articles => Set<Article>();
		public DbSet<Profile> Profiles => Set<Profile>();
		public DbSet<FollowMapping> FollowMappings => Set<FollowMapping>();
		public DbSet<Comment> Comments => Set<Comment>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
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
		}

		// TODO: Handle transaction
	}
}
