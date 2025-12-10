namespace ThoughtHub.Api.Models.Content
{
	/// <summary>
	/// Used to mark a class as a contnet block. Why use an attribute to store
	/// these properties. Because these properties belongs to the whole class
	/// (say HtmlBlock), not a single instance. So, adding them to the Block class itself
	/// doesn't make much sense.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class BlockTypeAttribute : Attribute
	{
		public string Name { get; set; }

		public string Category { get; set; }

		public string Icon { get; set; }

		public string Component { get; set; }
	}
}
