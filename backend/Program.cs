using backend.Data;
using backend.Data.Identity;
using backend.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO: Configure connection string and database provider
builder.Services.AddDbContext<PlatformContext>(options =>
{
	// TODO: Add the ability to use other providers
	options.UseSqlServer(builder.Configuration["ConnectionStrings:PlatformConnection"]);

	if (builder.Environment.IsDevelopment())
	{
		options.EnableSensitiveDataLogging(true);
	}
});

builder.Services
	.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

/*
 * AddIdentity adds everything AddIdentityCore adds, with some extra services,
 * namely Cookie Schemes (Application, External, and 2FA Schemes are all registered),
 * SignInManager, and RoleManager. Note that AddIdentity adds Role services
 * automatically, unlike AddDefaultIdentity.
 */
builder.Services
	.AddIdentityCore<User>()
	.AddEntityFrameworkStores<PlatformContext>()
	.AddApiEndpoints();

// TODO: Add Localization

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

builder.Services.AddCors(
	options => options.AddPolicy(
		"wasm",
		policy => policy.WithOrigins([builder.Configuration["PlatformUrls:BackendUrl"] ?? "http://localhost:5120",
			builder.Configuration["PlatformUrls:BlazorWasmFrontendUrl"] ?? "http://localhost:5220"])
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials()));

builder.Services.AddControllers();

/*
 * "You define the configuration using profiles. And then you let AutoMapper
 * know in what assemblies are those profiles defined by calling the IServiceCollection
 * extension method AddAutoMapper at startup"
 */
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

var app = builder.Build();

// TODO:EXPLAIN
app.MapIdentityApi<User>();
app.MapEmailLoginEndpoint();

// TODO:EXPLAIN
app.UseCors("wasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/logout", async (SignInManager<User> signInManager, [FromBody] object empty) =>
{
	if (empty is not null)
	{
		await signInManager.SignOutAsync();

		return Results.Ok();
	}

	return Results.Unauthorized();
}).RequireAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	//app.UseSwagger();
	//app.UseSwaggerUI();
	//app.MapOpenApi();

	await using var scope = app.Services.CreateAsyncScope();
	await SeedData.PopulateDatabase(scope.ServiceProvider);
}

app.Run();