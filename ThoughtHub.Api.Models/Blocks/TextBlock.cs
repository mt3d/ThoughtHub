using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Api.Models.Blocks
{
	public class TextBlock : BlockModel
	{
		public override string Name => "Text";

		public override string Category => "Content";

		public override string Icon => "bi bi-fonts";

		public override string Component => "text-block";
	}
}
