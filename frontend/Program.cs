using frontend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies")
	.AddCookie("Cookies", options =>
	{
		options.LoginPath = "/Account/Signin";
		options.LogoutPath = "/Account/Signout";
		options.AccessDeniedPath = "/Account/Denied";
		options.ExpireTimeSpan = TimeSpan.FromDays(30); // cookie lifetime
		options.SlidingExpiration = true; // optional
	});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<AuthApiClient>(client =>
{
	client.BaseAddress = new Uri("http://localhost:5000");
});

builder.Services.AddHttpClient("ApiClient", client =>
{
	client.BaseAddress = new Uri("http://localhost:5000"); // your API base URL
});

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
