using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Framework
{
	public partial class HubLayout : HubComponentBase
	{
		/// <summary>
		/// Stores the custom CSS classes for the different parts of the layout.
		/// </summary>
		public HubLayoutClasses Classes { get; set; }

		[Parameter]
		public RenderFragment? Header { get; set; }

		[Parameter]
		public RenderFragment? Footer { get; set; }

		[Parameter]
		public RenderFragment? Main { get; set; }

		[Parameter]
		public bool RemoveNavigationPane { get; set; }

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

		protected override string RootElementClass => "hub-layout";

		/// <summary>
		/// This is used for conditional registering of some css classes.
		/// </summary>
		protected override void RegisterCssClasses()
		{
			ClassBuilder
				.Register(() => Classes?.Root)
				.Register(() => StickyHeader ? "hub-layout-sticky-header" : string.Empty)
				.Register(() => StickyFooter ? "hub-layout-sticky-footer" : string.Empty);
		}
	}
}
