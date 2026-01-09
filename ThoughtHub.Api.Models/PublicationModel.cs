namespace ThoughtHub.Api.Models
{
	public class PublicationModel
	{
		public int PublicationId { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string PublicationImageUrl { get; set; } = string.Empty;
	}
}
