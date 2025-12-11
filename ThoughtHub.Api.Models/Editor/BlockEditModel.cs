using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Api.Models.Editor
{
	public class BlockEditModel
	{
		public BlockModel Block { get; set; }

		// The following properties are all BlockMeta.

		// General contnet meta

		// Unique client id
		public string Uid { get; set; } = "uid-" + Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

		public string Name { get; set; }

		public string Title { get; set; }

		public string Icon { get; set; }

		public string Description { get; set; }

		public string Placeholder { get; set; }

		// Client component
		public string Component { get; set; }

		// Block meta

		public bool IsCollapsed { get; set; }

		public bool IsReadonly { get; set; }

		public bool ShowHeader { get; set; } = true;

		public string EditorWidth { get; set; }
	}
}
