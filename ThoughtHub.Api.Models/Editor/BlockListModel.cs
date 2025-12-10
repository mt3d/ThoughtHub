namespace ThoughtHub.Api.Models.Editor
{
	public class BlockListModel
	{
		public class BlockListItem
		{
			public string Name { get; set; }

			public string Icon { get; set; }

			public string Type { get; set; }
		}

		// For example, we got Content, Media, and References blocks.
		public class BlockListCategory
		{
			public string Name { get; set; }

			public IList<BlockListItem> Items { get; set; } = new List<BlockListItem>();
		}

		public IList<BlockListCategory> Categories = new List<BlockListCategory>();

		public int TypeCount { get; set; }
	}
}
