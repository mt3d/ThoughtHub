using frontend_blazorwasm.Infrastructure.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace frontend_blazorwasm.Infrastructure
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
				var userDto = JsonSerializer.Deserialize<UserDto>(userJson, jsonSerializerOptions);

				if (userDto != null)
				{
					var claims = new List<Claim>
					{
						new (ClaimTypes.Name, userDto.Username),
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
			string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

			try
			{
				var result = await httpClient.PostAsJsonAsync("register", new { email, password });

				if (result.IsSuccessStatusCode)
				{
					return new FormResult { Succeeded = true };
				}

				// Explain why the signup failed in some detail.

				var responseDetails = await result.Content.ReadAsStringAsync();
				var problemDetails = JsonDocument.Parse(responseDetails);
				var errors = new List<string>();

				return new FormResult
				{
					Succeeded = false,
					ErrorList = problemDetails == null ? defaultDetail : [.. errors]
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
			return authenticated;
		}
	}
}
