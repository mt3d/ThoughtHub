
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ThoughtHub.UI.BlazorWasm.Components.FeedRightSection
{
	public sealed class StickySidebarOptions
	{
		public int FullNavbarHeight { get; set; }
		public int UpsellHeight { get; set; }
		public int BaseTopOffset { get; set; }
		public string? MinHeight { get; set; }
	}

	public partial class HubStickyBar : ComponentBase, IAsyncDisposable
	{
		[Inject]
		private IJSRuntime JS { get; set; } = default!;

		[Parameter]
		public string Display { get; set; } = "block";

		[Parameter]
		public RenderFragment ChildContent { get; set; } = default!;

		[Parameter]
		public int FullNavbarHeight { get; set; } = 0;

		[Parameter]
		public int UpsellHeight { get; set; } = 0;

		[Parameter]
		public int BaseTopOffset { get; set; } = 0;

		[Parameter]
		public string? MinHeight { get; set; }

		// Multiple instances work because IDs are unique
		private readonly string _sidebarId = "sticky-sidebar-{Guid.NewGuid()}";
		private readonly string _innerId = "sticky-inner-{Guid.NewGuid()}";

		private bool _isInitialized;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (!firstRender || _isInitialized)
			{
				return;
			}

			_isInitialized = true;

			var options = new StickySidebarOptions
			{
				FullNavbarHeight = FullNavbarHeight,
				UpsellHeight = UpsellHeight,
				BaseTopOffset = BaseTopOffset,
				MinHeight = MinHeight
			};

			await JS.InvokeVoidAsync(
				"stickySidebar.init",
				_sidebarId,
				_innerId,
				options);
		}

		public async ValueTask DisposeAsync()
		{
			if (_isInitialized)
			{
				await JS.InvokeVoidAsync("stickySidebar.dispose");
			}
		}
	}
}
