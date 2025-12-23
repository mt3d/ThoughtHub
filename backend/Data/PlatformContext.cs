using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Core.Entities.ReadingList;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Data.Entities.Publications;
using ThoughtHub.Data.Identity;

namespace ThoughtHub.Data
{
	public class PlatformContext : IdentityDbContext<User>
	{
		public PlatformContext(DbContextOptions<PlatformContext> options) : base(options) { }

		public DbSet<Article> Articles => Set<Article>();
		public DbSet<ArticleRevision> ArticleRevisions => Set<ArticleRevision>();
		public DbSet<ArticleBlock> ArticleBlocks => Set<ArticleBlock>();
		public DbSet<Block> Blocks => Set<Block>();
		public DbSet<BlockField> BlockFields => Set<BlockField>();

		public DbSet<Tag> Tags => Set<Tag>();
		public DbSet<Comment> Comments => Set<Comment>();

		public DbSet<Profile> Profiles => Set<Profile>();
		public DbSet<FollowMapping> FollowMappings => Set<FollowMapping>();

		public DbSet<Publication> Publications => Set<Publication>();
		public DbSet<PublicationFollower> PublicationFollowers => Set<PublicationFollower>();
		public DbSet<PublicationMember> PublicationMembers => Set<PublicationMember>();

		public DbSet<ReadingHistory> ReadingHistories => Set<ReadingHistory>();

		public DbSet<Image> Images => Set<Image>();
		public DbSet<ImageVersion> ImageVersions => Set<ImageVersion>();
		public DbSet<ImageFolder> ImageFolders => Set<ImageFolder>();

		public DbSet<ReadingList> ReadingLists => Set<ReadingList>();
		public DbSet<ReadingListArticle> ReadingListArticles => Set<ReadingListArticle>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			// This line is critical! It sets up keys for Identity tables.
			base.OnModelCreating(builder);

			FollowMapping.OnModelCreating(builder);
			Publication.OnModelCreating(builder);
			Profile.OnModelCreating(builder);
			ReadingHistory.OnModelCreating(builder);
			Tag.OnModelCreating(builder);
			ImageFolder.OnModelCreating(builder);
			Image.OnModelCreating(builder);
			ReadingList.OnModelCreating(builder);
			ReadingListArticle.OnModelCreating(builder);
		}

		// TODO: Handle transaction
	}
}
