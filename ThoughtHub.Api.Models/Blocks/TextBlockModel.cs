using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Api.Models.Blocks
{
	[BlockType(Name = "Text", Category = "Content", Icon = "bi bi-fonts", Component = "text-block")]
	public class TextBlockModel : BlockModel
	{
		// TODO: A simple "string" field.
		public string Body { get; set; }
	}
}
