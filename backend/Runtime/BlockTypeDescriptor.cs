namespace ThoughtHub.Runtime
{
	/// <summary>
	/// Each block type (say HtmlBlock, TextBlock, etc.) will have a corresponding
	/// type descriptor registered in the BlocksRegistry.
	/// </summary>
	public class BlockTypeDescriptor
	{
		public string Name { get; set; }

		public string Category { get; set; }

		public string Icon { get; set; }

		public bool IsGeneric { get; set; }

		public string Component { get; set; }

		public Type Type { get; set; }

		public string TypeName { get; set; }
	}
}
