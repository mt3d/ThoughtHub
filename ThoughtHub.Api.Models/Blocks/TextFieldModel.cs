using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Api.Models.Blocks
{
	[FieldType(Name = "Text", EditorComponent = "text-field")]
	public class TextFieldModel : IFieldModel
	{
		public string Value { get; set; }

		public static implicit operator TextFieldModel(string value)
		{
			return new TextFieldModel { Value = value };
		}

		public static implicit operator string (TextFieldModel field)
		{
			return field.Value;
		}
	}
}
