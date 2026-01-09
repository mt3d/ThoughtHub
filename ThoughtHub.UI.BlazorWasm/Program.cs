using ThoughtHub.UI.BlazorWasm;
using ThoughtHub.UI.BlazorWasm.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ThoughtHub.UI.BlazorWasm.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// TODO: Explain.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// TODO: Redesign authentication.
builder.Services.AddTransient<CookieHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

// set base address for default host
// TODO:EXPLAIN
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// TODO: Explain.
builder.Services.AddScoped<ViewportService>();

builder.AddCustomApplicationServices();

await builder.Build().RunAsync();
