using backend.Data.Identity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Infrastructure
{
	public static class EndpointRouteBuilderExtensions
	{
		/// <summary>
		/// An alternative implementation of the login endpoint provided by Identity Framework.
		/// It supports login by email instead of username.
		/// 
		/// Currently, there is no elegant way to remove the default endpoints provided by Microsoft.
		/// 
		/// The code is copy and pasted from Microsoft's implementation.
		/// </summary>
		/// <param name="routes"></param>
		/// <returns></returns>
		public static IEndpointRouteBuilder MapEmailLoginEndpoint(this IEndpointRouteBuilder routes)
		{
			routes.MapPost("/login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
			([FromBody] LoginRequest login,
			[FromQuery] bool? useCookies,
			[FromQuery] bool? useSessionCookies,
			[FromServices] IServiceProvider sp) =>
			{
				var signInManager = sp.GetRequiredService<SignInManager<User>>();

				var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
				var isPersistent = (useCookies == true) && (useSessionCookies != true);
				signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

				var userManager = sp.GetRequiredService<UserManager<User>>();
				var user = await userManager.FindByEmailAsync(login.Email);

				var result = await signInManager.PasswordSignInAsync(user?.UserName ?? login.Email, login.Password, isPersistent, lockoutOnFailure: true);

				if (result.RequiresTwoFactor)
				{
					if (!string.IsNullOrEmpty(login.TwoFactorCode))
					{
						result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
					}
					else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
					{
						result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
					}
				}

				if (!result.Succeeded)
				{
					return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
				}

				// The signInManager already produced the needed response in the form of a cookie or bearer token.
				return TypedResults.Empty;
			}).WithOrder(-1); // This is just feels ugly. TODO: Look for alternatives.

			return routes;
		}
	}
}
