
using ThoughtHub.Services;

namespace ThoughtHub.Events
{
	public class DefaultReadingListOnProfileCreationHandler : IEventHandler<ProfileCreatedEvent>
	{
		private readonly IReadingListService _readingListService;

		public DefaultReadingListOnProfileCreationHandler(IReadingListService readingListService)
		{
			_readingListService = readingListService;
		}

		public async Task HandleAsync(ProfileCreatedEvent e, CancellationToken ct)
		{
			await _readingListService.EnsureDefaultReadingListAsync(e.ProfileId);
		}
	}
}
