namespace ThoughtHub.Api.Models.Content
{
	public abstract class BlockModel
	{

		public string Name { get; set; }

		public string Category { get; set; }

		public string Icon { get; set; }

		public bool IsGeneric { get; set; }

		public string Component { get; set; }



		public Guid Id { get; set; }

		/// <summary>
		/// Block type Id.
		/// </summary>
		public string Type { get; set; }

		// TODO: Add a method to return the title
	}
}
