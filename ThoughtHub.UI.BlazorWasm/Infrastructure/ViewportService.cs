using Microsoft.JSInterop;

namespace ThoughtHub.UI.BlazorWasm.Infrastructure
{
	public class ViewportService : IAsyncDisposable
	{
		private readonly IJSRuntime _js;
		private DotNetObjectReference<ViewportService>? _ref;
		private IJSObjectReference? _unsubscribe;

		public int Width { get; private set; }

		public event Action? Changed;

		public ViewportService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task InitializeAsync()
		{
			Width = await _js.InvokeAsync<int>("viewport.getWidth");

			_ref = DotNetObjectReference.Create(this);
			_unsubscribe = await _js.InvokeAsync<IJSObjectReference>("viewport.subscribe", _ref);
		}

		[JSInvokable]
		public void OnViewportChanged(int width)
		{
			Width = width;
			Changed?.Invoke();
		}

		public async ValueTask DisposeAsync()
		{
			if (_unsubscribe is not null)
			{
				await _unsubscribe.InvokeVoidAsync("call");
				await _unsubscribe.DisposeAsync();
			}

			_ref?.Dispose();
		}
	}
}
