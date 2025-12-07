using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ThoughtHub.Tools.DataSeeder
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
			var host = CreateHostBuilder(args, environment).Build();

			try
			{
				var seedtype = args.Length > 0 ? args[0] : "all";
				var count = args.Length > 1 && int.TryParse(args[1], out int c) ? c : 1000;

				var seeder = host.Services.GetRequiredService<MainSeeder>();

				switch (seedtype.ToLower())
				{
					case "all":
					default:
						await seeder.SeedAllAsync(count);
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred during seeding: {ex.Message}");
				Environment.Exit(1);
			}
		}

		static IHostBuilder CreateHostBuilder(string[] args, string environment)
		{
			return Host.CreateDefaultBuilder(args)
				/**
				 * The host configuration is available in HostBuilderContext.Configuration within the ConfigureAppConfiguration method.
				 * When you call the ConfigureAppConfiguration method, the HostBuilderContext and IConfigurationBuilder are passed into the configureDelegate.
				 * The configureDelegate is defined as an Action<HostBuilderContext, IConfigurationBuilder>.
				 * 
				 * The HostBuilderContext exposes the Configuration property, which is an instance of IConfiguration. It represents the configuration built from the host, whereas the IConfigurationBuilder is the builder object used to configure the app.
				 */
				.ConfigureAppConfiguration((context, config) =>
				{
					config.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json", optional: false)
						.AddJsonFile($"appsettings.{environment}.json", optional: true)
						.AddEnvironmentVariables();
				})
				/**
				 * Use the ConfigureServices method to add services to the Microsoft.Extensions.DependencyInjection.IServiceCollection instance. These services are used to build an IServiceProvider that's used with dependency injection to resolve the registered services.
				 */
				.ConfigureServices((context, services) =>
				{
					//services.AddDbContext<PlatformContext>();

					/**
					 * With Microsoft Extensions, DI is managed by adding services and
					 * configuring them in an IServiceCollection. The IHost interface exposes
					 * the IServiceProvider instance, which acts as a container of all the
					 * registered services.
					 */
					services.AddScoped<MainSeeder>();
					//services.AddScoped<ImageSeeder>();
					//services.AddScoped<ProfileSeeder>();
					services.AddScoped<IImageGenerator, JdenticonImageGenerator>();
				});
		}
	}
}
