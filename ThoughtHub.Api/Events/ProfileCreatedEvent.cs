namespace ThoughtHub.Events
{
	public sealed record ProfileCreatedEvent(
		Guid ProfileId,
		DateTime CreatedAtUtc);
}
