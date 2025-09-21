using backend.Data.Identity;

namespace backend.Data.Entities
{
	public class FollowMapping
	{
		public int FollowerId { get; init; }
		public Profile? Follower { get; init; }

		public int FolloweeId { get; init; }
		public Profile? Followee { get; init; }
	}
}
