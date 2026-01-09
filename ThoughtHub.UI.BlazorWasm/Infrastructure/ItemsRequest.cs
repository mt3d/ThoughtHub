namespace ThoughtHub.UI.BlazorWasm.Infrastructure
{
	/// <summary>
	/// Represents a request for new items.
	/// </summary>
	public sealed class ItemsRequest
	{
		public ItemsRequest(int startIndex, CancellationToken cancellationToken)
		{
			StartIndex = startIndex;
			CancellationToken = cancellationToken;
		}

		public int StartIndex { get; }
		public CancellationToken CancellationToken { get; }
	}

	// Represents a function that fulfills an ItemsRequest.
	public delegate Task<IEnumerable<T>> ItemsProvider<T>(ItemsRequest request);
}
