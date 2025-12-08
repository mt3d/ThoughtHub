namespace ThoughtHub.Api.Models.Content
{
	public abstract class BlockModel
	{
		public Guid Id { get; set; }

		/// <summary>
		/// Block type Id.
		/// </summary>
		public string Type { get; set; }

		// TODO: Add a method to return the title
	}
}
