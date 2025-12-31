namespace ThoughtHub.Events
{
	public class InProcessEventPublisher : IEventPublisher
	{
		private readonly IServiceProvider _serviceProvider;

		public InProcessEventPublisher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task PublishAsync<TEvent>(TEvent e, CancellationToken ct = default) where TEvent : class
		{
			var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

			foreach (var handler in handlers)
			{
				await handler.HandleAsync(e, ct);
			}
		}
	}
}
