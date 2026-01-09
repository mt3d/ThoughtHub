using ThoughtHub.Data.Entities;

namespace ThoughtHub.Services
{
	public interface ICurrentUserService
	{
		string UserId { get; }

		bool IsAuthenticated { get; }

		Task<Profile> GetProfileAsync();
	}
}
