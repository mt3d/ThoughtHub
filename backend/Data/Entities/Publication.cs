namespace backend.Data.Entities
{
	public class Publication
	{
		public int PublicationId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
	}
}
