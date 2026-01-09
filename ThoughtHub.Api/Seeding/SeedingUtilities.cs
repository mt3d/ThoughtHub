using System.Text.RegularExpressions;

namespace ThoughtHub.Seeding
{
	public static class SeedingUtilities
	{
		public static string Slugify(string text)
		{
			text = text.ToLowerInvariant();

			// Remove invalid characters
			// ^ inside [] negates it, meaning match any character not listed.
			// \s → any whitespace (space, tab, etc.)
			text = Regex.Replace(text, @"[^a-z0-9\s-]", "");

			// Replace spaces with dash
			text = Regex.Replace(text, @"\s+", "-");

			// Collapse multiple dashes
			text = Regex.Replace(text, @"-+", "-");

			return text;
		}
	}
}
