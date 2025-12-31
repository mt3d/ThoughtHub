using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Events
{
	public class ProfileCreationOnRegistration : IEventHandler<UserRegisteredEvent>
	{
		// TODO: Use ProfileService.
		private readonly PlatformContext _context;
		private readonly IEventPublisher _eventPublisher;

		public ProfileCreationOnRegistration(
			PlatformContext context,
			IEventPublisher eventPublisher)
		{
			_context = context;
			_eventPublisher = eventPublisher;
		}

		public async Task HandleAsync(UserRegisteredEvent e, CancellationToken ct)
		{
			var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == e.UserId);

			if (profile is null)
			{
				profile = new Profile
				{
					UserId = e.UserId,
					CreatedAtUtc = DateTime.UtcNow,
					UpdatedAtUtc = DateTime.UtcNow,
				};

				_context.Profiles.Add(profile);
				await _context.SaveChangesAsync();

				await _eventPublisher.PublishAsync(new ProfileCreatedEvent
				(
					profile.Id,
					DateTime.UtcNow
				), ct);
			}
		}
	}
}
