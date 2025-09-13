using System.Text;
using System.Text.Json;

namespace frontend
{
	public class UserResponse
	{
		public string Email { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Bio { get; set; } = string.Empty;
		public string ProfilePic { get; set; } = string.Empty;
	}

	public class LoginResponse
	{
		public UserResponse User { get; set; } = new();
	}

	public class AuthApiClient
	{
		private readonly HttpClient client;

		public AuthApiClient(HttpClient client)
		{
			this.client = client;
		}

		public async Task<UserResponse> SigninAsync(string email, string password)
		{
			var payload = new { email, password };

			StringContent content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync("/authentication/signin", content);
			response.EnsureSuccessStatusCode();

			string stringResponse = await response.Content.ReadAsStringAsync();
			UserResponse userResponse = JsonSerializer.Deserialize<UserResponse>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!; // successful response

			return userResponse;
		}

		public async Task<HttpResponseMessage> RegisterAsync(string username, string email, string password)
		{
			var payload = new { username, email, password };

			StringContent content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

			return await client.PostAsync("/authentication/signup", content);
		}
	}
}
