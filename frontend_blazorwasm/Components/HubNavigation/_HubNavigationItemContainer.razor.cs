using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.HubNavigation
{
	public partial class _HubNavigationItemContainer
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public string? Class { get; set; }

		[Parameter]
		public string? Style { get; set; }

		[Parameter]
		public bool Disabled { get; set; }

		[Parameter]
		public string? Href { get; set; }

		[Parameter]
		public EventCallback OnClick { get; set; }

		//[Parameter] public string? Rel { get; set; }

		/// <summary>
		/// Gets or sets whether the container should be rendered as a link or a button.
		/// This depends on whether it has a url or not.
		/// </summary>
		[Parameter]
		public bool RenderLink { get; set; }

		[Parameter]
		public string? Target { get; set; }

		[Parameter]
		public string? Title { get; set; }
	}
}
