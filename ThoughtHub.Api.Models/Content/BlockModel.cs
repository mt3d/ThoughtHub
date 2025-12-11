using System.Text.Json.Serialization;
using ThoughtHub.Api.Models.Blocks;

namespace ThoughtHub.Api.Models.Content
{
	/// <summary>
	/// Each article contains a list of blocks. In order to provid an easy access
	/// to the underlying type (say HtmlBlock, TextBlock, etc.), a Type string property
	/// is the only member property in this class (besides the Id).
	/// </summary>
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
	[JsonDerivedType(typeof(TextBlockModel), "TextBlockModel")]
	public abstract class BlockModel
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Block type Id.
		/// </summary>
		public string Type { get; set; }
	}
}
