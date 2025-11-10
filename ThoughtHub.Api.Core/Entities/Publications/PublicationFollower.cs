namespace ThoughtHub.Data.Entities.Publications
{
	public class PublicationFollower
	{
		public int PublicationFollowerId { get; set; }

		public int PublicationId { get; set; }

		public Publication Publication { get; set; }

		public int ProfileId { get; set; }

		public Profile Profile { get; set; }

		/// <summary>
		/// Gets or sets when the profile started following the publication.
		/// </summary>
		public DateTime FollowedAt { get; set; }
	}
}
