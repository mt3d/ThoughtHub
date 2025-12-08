using System.Text.Json.Serialization;

namespace ThoughtHub.Api.Core.Entities.Article
{
	public sealed class BlockField
	{
		public Guid Id { get; set; }

		public Guid BlockId { get; set; }

		public string FieldId { get; set; }

		public int SortOrder { get; set; }

		public string ClrType { get; set; }

		public string SerializedValue { get; set; }

		/// <summary>
		/// The block containing this field.
		/// </summary>
		[JsonIgnore]
		public Block Block { get; set; }
	}
}
