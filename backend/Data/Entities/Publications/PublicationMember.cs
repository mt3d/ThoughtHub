using backend.Data.Entities;

namespace ThoughtHub.Data.Entities.Publications
{
	public class PublicationMember
	{
		public int PublicationMemberId { get; set; }

		public int PublicationId { get; set; }

		public Publication Publication { get; set; }

		public int ProfileId { get; set; }

		public Profile Member { get; set; }

		public PublicationRole Role { get; set; }

		public DateTime JoinedAt { get; set; }
	}
}
