using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Framework
{
	public partial class _HubNavigationChild<TItem> where TItem : class
	{
		[CascadingParameter]
		private HubNavigation<TItem> ParentNav { get; set; }

		[CascadingParameter]
		private _HubNavigationChild<TItem>? Parent { get; set; }

		[Parameter]
		public TItem Item { get; set; }
	}
}
