namespace frontend.Models
{
	public class Publication
	{
		public string Name { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;

		public int FollowersCount { get; set; }
		public DateTime LastPublished { get; set; } // 1 hour ago
		public string Description { get; set; } = string.Empty;
	}
}
