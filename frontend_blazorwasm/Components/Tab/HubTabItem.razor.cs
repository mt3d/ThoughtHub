using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Tab
{
	public partial class HubTabItem : HubComponentBase
	{
		[CascadingParameter]
		private HubTab? _parent { get; set; }

		[Parameter]
		public RenderFragment? Header { get; set; }

		[Parameter]
		public string? HeaderText { get; set; }

		[Parameter]
		public RenderFragment? Body { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public string? Key { get; set; }

		protected override string RootElementClass => "hub-tab-item";

		protected override async Task OnInitializedAsync()
		{
			_parent?.RegisterItem(this);

			await base.OnInitializedAsync();
		}

		// Two way bound.
		public bool IsSelected { get; set; }

		internal void SetIsSelected(bool value)
		{
			IsSelected = true;
			StateHasChanged();
		}

		private async Task HandleOnClick()
		{
			// TODO: Check if the tab is enabled.

			_parent?.SelectItem(this);
		}
	}
}
