using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.NavigationPane
{
	public partial class HubNavigationPane : HubComponentBase
	{
		// TODO: Class builder should be reset after changing this value.
		[Parameter]
		public bool IsOpen { get; set; }

		[Parameter]
		public EventCallback<bool> IsOpenChanged { get; set; }

		// For this, the class builder doesn't need rebuilding.
		[Parameter]
		public bool IsToggled { get; set; }

		[Parameter]
		public EventCallback<bool> IsToggledChanged { get; set; }

		// TODO: Copy-pasted from the book. Change the name to Close() for instance.
		public async Task HandleSelect()
		{
			//IsOpen = e.Value as bool;
			await IsOpenChanged.InvokeAsync(IsOpen);
		}

		protected override string RootElementClass => "hub-navigation-pane";

		protected override void RegisterCssClasses()
		{
			ClassBuilder.Register(() => IsOpen == true ? string.Empty : "hub-navigation-pane-closed");
		}

		protected override void OnParametersSet()
		{
			ClassBuilder.Reset();

			base.OnParametersSet();
		}
	}
}
