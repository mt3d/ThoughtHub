using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Framework
{
	public partial class HubTab : HubComponentBase
	{
		private HubTabItem? _selectedItem;
		private List<HubTabItem> _items = [];

		[Parameter]
		public HubTabClassesStyles Classes { get; set; }

		[Parameter]
		public HubTabClassesStyles Styles { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public bool RenderAll { get; set; } = false;

		[Parameter]
		public HubTabPosition? Position { get; set; }

		[Parameter]
		public string? SelectedKey { get; set; }

		[Parameter]
		public EventCallback<string?> SelectedKeyChanged { get; set; }

		protected override string RootElementClass => "hub-tab";

		protected override void RegisterCssClasses()
		{
			ClassBuilder.Register(() => Position switch
			{
				HubTabPosition.Top => "hub-tab-top",
				HubTabPosition.Bottom => "hub-tab-bottom",
				HubTabPosition.Start => "hub-tab-start",
				HubTabPosition.End => "hub-tab-end",
				_ => "hub-tab-top"
			});
			base.RegisterCssClasses();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// This method is called by the tab items when they the user clicks on them.
		/// </remarks>
		internal async void SelectItem(HubTabItem item)
		{
			_selectedItem?.SetIsSelected(false);
			item.SetIsSelected(true);

			_selectedItem = item;
			await AssignSelectedKey(item.Key);

			// TODO: Raise a tab change event.

			StateHasChanged();
		}

		internal void RegisterItem(HubTabItem item)
		{
			if (SelectedKey is null)
			{
				if (_items.Count == 0)
				{
					item.SetIsSelected(true);
					_selectedItem = item;
					StateHasChanged();
				}
			}
			else if (SelectedKey == item.Key)
			{
				item.SetIsSelected(true);
				_selectedItem = item;
				StateHasChanged();
			}

			_items.Add(item);
		}

		private async Task AssignSelectedKey(string? value)
		{
			SelectedKey = value;
			await SelectedKeyChanged.InvokeAsync(value);
		}
	}
}
