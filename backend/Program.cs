using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ThoughtHub.Api.LocalStorage;
using ThoughtHub.Api.LocalStorage.Extensions;
using ThoughtHub.Data;
using ThoughtHub.Data.Identity;
using ThoughtHub.Infrastructure;
using ThoughtHub.Seeding;
using ThoughtHub.Services;
using ThoughtHub.Storage;

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

builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddLocalFileStorage(builder.Configuration["PlatformUrls:BackendUrl"] + "/uploads/");

// TODO: Only if development.
builder.Services.AddScoped<ProfileSeeder>();
builder.Services.AddScoped<TagSeeder>();
builder.Services.AddScoped<PublicationSeeder>();
builder.Services.AddScoped<ArticleSeeder>();
builder.Services.AddScoped<MainSeeder>();

/*
 * "You define the configuration using profiles. And then you let AutoMapper
 * know in what assemblies are those profiles defined by calling the IServiceCollection
 * extension method AddAutoMapper at startup"
 */
builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting(); // Necessary

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

	try
	{
		//var seedtype = args.Length > 0 ? args[0] : "all";
		//var count = args.Length > 1 && int.TryParse(args[1], out int c) ? c : 1000;

		await using var scope = app.Services.CreateAsyncScope();

		var context = scope.ServiceProvider.GetRequiredService<PlatformContext>();
		await context.Database.EnsureDeletedAsync();
		await context.Database.EnsureCreatedAsync();

		MainSeeder seeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
		await seeder.SeedAllAsync();

		//switch (seedtype.ToLower())
		//{
		//	case "all":
		//	default:
		//		await seeder.SeedAllAsync(count);
		//		break;
		//}
	}
	catch (Exception ex)
	{
		Console.WriteLine($"An error occurred during seeding: {ex.Message}");
		Environment.Exit(1);
	}


}

app.Run();