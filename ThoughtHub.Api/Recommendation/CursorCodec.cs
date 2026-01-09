using System.Text;
using System.Text.Json;

namespace ThoughtHub.Recommendation
{
	public static class CursorCodec
	{
		public static string Encode(CursorPayload payload)
		{
			var json = JsonSerializer.Serialize(payload);

			return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
		}

		public static CursorPayload? Decode(string cursor)
		{
			var json = Encoding.UTF8.GetString(Convert.FromBase64String(cursor));

			return JsonSerializer.Deserialize<CursorPayload>(json);
		}
	}
}
