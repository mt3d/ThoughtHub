using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.HubNavigation
{
	public partial class HubNavigation<TItem> : HubComponentBase where TItem : class
	{
		internal TItem? _currentItem;
		internal List<TItem> _items = [];

		protected override string RootElementClass => "hub-navigation";

		[Parameter]
		public IList<TItem> Items { get; set; } = [];

		[Parameter]
		public HubNavigationRenderType RenderType { get; set; } = HubNavigationRenderType.Normal;

		[Parameter]
		public int IndentValue { get; set; } = 16;

		//private void OnSetParameters()
		//{
		//	_items = Items?.ToList() ?? [];

		//	// Set the selected item by the current url
		//}

		protected override void OnParametersSet()
		{
			_items = Items?.ToList() ?? [];
		}

		// Many elements could be stored in the navigation component.
		// That's why such a method is needed
		internal string? GetUrl(TItem item)
		{
			if (item is HubNavigationItem navItem)
			{
				return navItem.Url;
			}

			// TODO: Hanlde other cases

			return null;
		}

		internal string? GetText(TItem item)
		{
			if (item is HubNavigationItem navItem)
			{
				return navItem.Text;
			}

			return null;
		}

		internal string? GetIconName(TItem item)
		{
			if (item is HubNavigationItem navItem)
			{
				return navItem.IconName;
			}

			return null;
		}

		internal bool GetIsSeparator(TItem item)
		{
			if (item is HubNavigationItem navItem)
			{
				return navItem.IsSeparator;
			}

			return false;
		}
	}
}
