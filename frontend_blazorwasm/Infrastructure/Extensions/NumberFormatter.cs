namespace frontend.Infrastructure
{
	public static class NumberFormatter
	{
		public static string ToCompactString(this int number)
		{
			if (number >= 1_000_000)
			{
				return (number / 1_000_000).ToString("0.#") + "M";
			}
			else if (number >= 1000)
			{
				// Divide by double to preserve the fractional part
				return (number / 1000D).ToString("0.#") + "K";
			}

			return number.ToString();
		}
	}
}
