namespace ThoughtHub.Events
{
	public sealed record UserRegisteredEvent(
		string UserId,
		string Email,
		DateTime RegisteredAtUtc);
}
