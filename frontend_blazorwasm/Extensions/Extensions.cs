using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ThoughtHub.UI.BlazorWasm.Infrastructure;
using ThoughtHub.UI.BlazorWasm.Services;

namespace ThoughtHub.UI.BlazorWasm.Extensions
{
	public static class Extensions
	{
		// TODO: WebAssemblyHostBuilder might be changed.
		public static void AddCustomApplicationServices(this WebAssemblyHostBuilder builder)
		{
			builder.Services.AddBlazoredModal();

			// Application services.

			// Http clients.

			// TODO: Handle API version.
			// TODO: Revise Auth token.
			//builder.Services.AddHttpClient(
			//	"Auth", opt => opt.BaseAddress = new("http://main-api"))
			//	.AddHttpMessageHandler<CookieHandler>();

			builder.Services.AddHttpClient<IArticleService, ArticleService>(opt =>
				{
					opt.BaseAddress = new Uri("http://localhost:5120");
				})
				.AddHttpMessageHandler<CookieHandler>();

			builder.Services.AddHttpClient(
				"Auth",
				opt => opt.BaseAddress = new Uri(builder.Configuration["ApplicationUrls:BackendUrl"] ?? "http://localhost:5120")
				).AddHttpMessageHandler<CookieHandler>();

		}
	}
}
