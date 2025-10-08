using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace frontend_blazorwasm.Infrastructure
{
	// DelegatingHandler is part of HttpClient's message handler pipeline
	// It lets you intercept ongoing Http requests or inspect incoming responses
	// Common uses: add headers, handle retries, attach tokens, logging.
	public class CookieHandler : DelegatingHandler
	{
		// This method is called every time HttpClient makes a request.
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			/*
			 * This is Blazor WebAssembly-specific (not in regular .NET).
			 * It sets how the browser should send credentials (like cookies, Authorization headers, TLS client certs).
			 * BrowserRequestCredentials.Include = “always send cookies with this request (even for cross-site requests)”.
			 * Equivalent to fetch(..., { credentials: "include" }) in JavaScript.
			 * Without this, Blazor WebAssembly HttpClient won’t send cookies by default.
			 */
			request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

			/*
			 * Adds a custom HTTP header X-Requested-With: XMLHttpRequest.
			 * Historically used to distinguish AJAX requests from normal browser navigation.
			 * Some servers or middleware check for this header to allow CSRF protection bypass (since it indicates it’s a JS/XHR call, not a form POST).
			 * In modern apps, it’s less critical but sometimes required if your backend expects it.
			 */
			request.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);

			// call base.SendAsync() to continue down the pipeline.
			return base.SendAsync(request, cancellationToken);
		}
	}
}
