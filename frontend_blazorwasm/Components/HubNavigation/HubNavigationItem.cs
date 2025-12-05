namespace ThoughtHub.UI.BlazorWasm.Components.HubNavigation
{
	public class HubNavigationItem
	{
		/// <summary>
		/// Custom css class
		/// </summary>
		public string? Class { get; set; }

		public List<HubNavigationItem> ChildItems { get; set; } = new();

		public string? Description { get; set; }

		public string? IconName { get; set; }

		public bool IsEnabled { get; set; } = true;

		public bool IsSeparator { get; set; }

		public string? Id { get; set; }

		/// <summary>
		/// How to open the navigation item's link.
		/// </summary>
		public string? Target { get; set; }

		public string Text { get; set; } = string.Empty;

		public string? Url { get; set; }
	}
}
