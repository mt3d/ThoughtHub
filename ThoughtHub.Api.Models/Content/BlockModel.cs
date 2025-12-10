namespace ThoughtHub.Api.Models.Content
{
	public abstract class BlockModel
	{

		public virtual string Name { get; set; }

		public virtual string Category { get; set; }

		public virtual string Icon { get; set; }

		public virtual bool IsGeneric { get; set; }

		public virtual string Component { get; set; }



		public Guid Id { get; set; }

		/// <summary>
		/// Block type Id.
		/// </summary>
		public string Type { get; set; }

		// TODO: Add a method to return the title
	}
}
