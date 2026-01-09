namespace ThoughtHub.UI.BlazorWasm.Infrastructure
{
	public static class DateTimeExtensions
	{
		public static string ToRelativeTime(this DateTime date)
		{
			TimeSpan diff = DateTime.UtcNow - date.ToUniversalTime();

			if (diff.TotalSeconds < 60)
			{
				return "just now";
			}
			if (diff.TotalMinutes < 60)
			{
				return $"{(int)diff.TotalMinutes} min ago";
			}
			if (diff.TotalHours < 24)
			{
				return $"{(int)diff.TotalHours} hours ago";
			}
			if (diff.TotalDays < 30)
			{
				return $"{(int)diff.TotalDays} day{(diff.TotalDays >= 2 ? "s" : "")} ago";
			}
			if (diff.TotalDays < 365)
			{
				return $"{(int)diff.TotalDays / 30} month{(diff.TotalDays >= 30)} ago";
			}

			return $"{(int)(diff.TotalDays / 365)} year(s) ago";
		}
	}
}
