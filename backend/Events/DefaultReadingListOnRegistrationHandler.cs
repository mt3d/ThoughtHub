
using ThoughtHub.Services;

namespace ThoughtHub.Events
{
	public class DefaultReadingListOnRegistrationHandler : IEventHandler<UserRegisteredEvent>
	{
		private readonly ReadingListService _readingListService;


		public async Task HandleAsync(UserRegisteredEvent e, CancellationToken ct)
		{
			await _readingListService.EnsureDefaultReadingListAsync(e.ProfileId);
		}
	}
}
