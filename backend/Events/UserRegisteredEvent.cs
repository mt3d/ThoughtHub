namespace ThoughtHub.Events
{
	public sealed record UserRegisteredEvent(
		Guid ProfileId,
		string Email,
		DateTime RegisteredAtUtc);
}
