using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Data.Entities.Publications
{
	/// <summary>
	/// A Publication in Medium is essentially a mini-organization inside the platform:
	/// It has owners, editors, and writers.
	/// It has stories (posts), a homepage, and its own branding (logo, color, layout).
	/// It can invite writers, review submitted drafts, and publish under its own name.
	/// It has followers, tags, and SEO data.
	/// </summary>
	public class Publication
	{
		/// <summary>
		/// Gets or sets the ID of the publication.
		/// </summary>
		public int PublicationId { get; set; }

		/// <summary>
		/// Gets or sets the name of the publication.
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the slug of the publication.
		/// </summary>
		public string Slug { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets all the articles that are published in this publication
		/// </summary>
		public List<Article> Articles { get; set; } = new();

		/// <summary>
		/// Gets or sets the tag line of the publication.
		/// </summary>
		public string TagLine { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the long description of the publication.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the ID of the publication's owner.
		/// </summary>
		public int OwnerId { get; set; }

		/// <summary>
		/// Gets or sets the owner of the publication.
		/// </summary>
		public Profile Owner { get; set; }

		/// <summary>
		/// Gets or sets rows inside the joint table of Publication-Profile membership relationship.
		/// </summary>
		public ICollection<PublicationMember> PublicationMembers { get; set; } = new List<PublicationMember>();

		/// <summary>
		/// Gets or sets the members of the publication.
		/// </summary>
		public ICollection<Profile> Members { get; set; } = new List<Profile>();

		/// <summary>
		/// Gets or sets rows inside the joint table of Publication-Profile following relationship.
		/// </summary>
		public ICollection<PublicationFollower> PublicationFollowers { get; set; } = new List<PublicationFollower>();

		/// <summary>
		/// Gets or sets the followers of the publication.
		/// </summary>
		public ICollection<Profile> Followers { get; set; } = new List<Profile>();

		/// <summary>
		/// Gets or sets the creation time of the publication.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the last update time of the publication.
		/// </summary>
		public DateTime UpdatedAt { get; set; }

		public Image? PublicationImage { get; set; }

		public Guid? PublicationImageId { get; set; }

		// TODO: Add social media Urls
		// pulbic List<SocialMediaLink> SocialLinks = new();

		public static void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Publication>(builder =>
			{
				builder.HasMany(p => p.Articles)
				.WithOne(a => a.Publication)
				.HasForeignKey(a => a.PublicationId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(p => p.Slug)
				.IsUnique();

			builder.HasOne(p => p.Owner)
				.WithMany()
				.HasForeignKey(p => p.OwnerId)
				.OnDelete(DeleteBehavior.Restrict);
			});
		}
	}
}
