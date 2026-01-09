using ThoughtHub.UI.BlazorWasm.Infrastructure.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ThoughtHub.UI.BlazorWasm.Infrastructure
{
	public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory, ILogger<CookieAuthenticationStateProvider> logger)
			: AuthenticationStateProvider, IAccountManagement
	{
		//private IHttpClientFactory httpClientFactory;
		//private ILogger<CookieAuthenticationStateProvider> logger;

		//public CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory, ILogger<CookieAuthenticationStateProvider> logger)
		//{
		//	this.httpClientFactory = httpClientFactory;
		//	this.logger = logger;
		//}

		private readonly JsonSerializerOptions jsonSerializerOptions = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		private readonly HttpClient httpClient = httpClientFactory.CreateClient("Auth");
		private bool authenticated = false;
		private readonly ClaimsPrincipal unauthenticatedPrincipal = new(new ClaimsIdentity());

		// AuthenticationStateProvider methods

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			authenticated = false;

			// defaults to unauthenticated
			var user = unauthenticatedPrincipal;

			try
			{
				// manage/info is added MapIdentityApi.
				using var userResponse = await httpClient.GetAsync("manage/info");

				userResponse.EnsureSuccessStatusCode();

				var userJson = await userResponse.Content.ReadAsStringAsync();
				Console.WriteLine(userJson);

				var userDto = JsonSerializer.Deserialize<UserDto>(userJson, jsonSerializerOptions);

				if (userDto != null)
				{
					var claims = new List<Claim>
					{
						//new (ClaimTypes.Name, userDto.Username),
						new (ClaimTypes.Email, userDto.Email),
					};

					// TODO: Add the rest of the claims
					// TODO: Request the role

					var identity = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
					user = new ClaimsPrincipal(identity);
					authenticated = true;
				}
			}
			catch (Exception ex) when (ex is HttpRequestException exception)
			{
				if (exception.StatusCode != HttpStatusCode.Unauthorized)
				{
					logger.LogError(ex, "App error");
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "App error");
			}

			return new AuthenticationState(user);
		}

		// IAccountManagement methods

		public async Task<FormResult> RegisterAsync(string email, string password)
		{
			string[] defaultDetail = ["An unknown error occured during registration"];

			try
			{
				var result = await httpClient.PostAsJsonAsync("register", new { email, password });

				if (result.IsSuccessStatusCode)
				{
					return new FormResult { Succeeded = true };
				}

				// Explain why the signup failed in some detail.

				/**
				 * ASP.NET Core model validation response structure:
				 * {
				 *	"errors": {
				 *		"Email": [ "The Email field is required." ],
				 *		"Password": [ "The field Password must be at least 6 characters." ]
				 *		}
				 * }
				 */
				string content = await result.Content.ReadAsStringAsync();
				JsonDocument parsedContent = JsonDocument.Parse(content);
				var errors = new List<string>();
				var errorList = parsedContent.RootElement.GetProperty("errors");

				foreach (var entry in errorList.EnumerateObject())
				{
					if (entry.Value.ValueKind == JsonValueKind.String)
					{
						errors.Add(entry.Value.ToString());
					}
					else if (entry.Value.ValueKind == JsonValueKind.Array)
					{
						errors.AddRange(entry.Value.EnumerateArray()
							.Select(e => e.GetString() ?? string.Empty)
							.Where(e => !string.IsNullOrEmpty(e)));
					}
				}

				return new FormResult
				{
					Succeeded = false,
					ErrorList = parsedContent == null ? defaultDetail : [.. errors]
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "App error");
			}

			return new FormResult
			{
				Succeeded = false,
				ErrorList = defaultDetail
			};
		}

		public async Task<FormResult> SigninAsync(string email, string password)
		{
			try
			{
				var result = await httpClient.PostAsJsonAsync("login?useCookies=true", new { email, password });

				if (result.IsSuccessStatusCode)
				{
					NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

					return new FormResult { Succeeded = true };
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "App error");
			}

			return new FormResult
			{
				Succeeded = false,
				ErrorList = ["Invalid email and/or password"]
			};
		}

		public async Task SignoutAsync()
		{
			const string Empty = "{}";
			var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");

			await httpClient.PostAsync("logout", emptyContent);

			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public async Task<bool> CheckAuthenticatedState()
		{
			await GetAuthenticationStateAsync();

			Console.WriteLine(authenticated);

			return authenticated;
		}
	}
}
