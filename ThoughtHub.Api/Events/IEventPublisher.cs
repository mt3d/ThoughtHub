namespace ThoughtHub.Events
{
	public interface IEventPublisher
	{
		Task PublishAsync<TEvent>(TEvent e, CancellationToken ct = default) where TEvent : class;
	}
}
