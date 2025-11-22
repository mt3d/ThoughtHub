using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Layout
{
	public partial class HubLayout
	{
		[Parameter]
		public RenderFragment? Header { get; set; }

		[Parameter]
		public RenderFragment? Footer { get; set; }

		[Parameter]
		public RenderFragment? Main { get; set; }

		[Parameter]
		public bool HideNavigationPane { get; set; }

		[Parameter]
		public RenderFragment? NavigationPane { get; set; }

		[Parameter]
		public int NavigationPaneWidthPx { get; set; }

		[Parameter]
		public bool ReverseNavigationPane { get; set; }

		[Parameter]
		public bool StickyFooter { get; set; }

		[Parameter]
		public bool StickyHeader { get; set; }
	}
}
