using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend
{
	public static class ServiceExtensions
	{
		public static void AddJwtAuthentication(this IServiceCollection services)
		{
			// Define how incoming tokens are validated (think of it as the validation rule)
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
			{
				// They must be signed with our key
				ValidateIssuerSigningKey = true,

				// IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["jwtSecret"]!)),
				// TODO: Of course, the key in production should be stored somewhere else.
			    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a_long_enough_secret_for_this_algorithm")),
				ValidateAudience = false,
				ValidateIssuer = false
			};

			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = tokenValidationParameters;

					// TODO: Consider handling the case where the Authorization header is
					// Authorization: Token <token>
					// Use OnMessageReceived event to do that.

					// TODO: When using Identity framework in the future,
					// you will need to handle OnTokenValidated event
					// to replace ctx.Principal with one created by SignInManager,
					// in order to pull in all the full Identity claims (like roles,
					// email confirmed, custom claims, etc.).
				});
		}
	}
}
