namespace ThoughtHub.Api.Models.Content
{
	public class FieldTypeAttribute : Attribute
	{
		public string Name { get; set; }

		public string EditorComponent { get; set; }
	}
}
