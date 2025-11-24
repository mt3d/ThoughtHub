namespace ThoughtHub.Tools.DataSeeder
{
	internal class Program
	{
		// TODO: Create a host
		static void Main(string[] args)
		{
			try
			{
				var seedtype = args.Length > 0 ? args[0] : "all";
				var count = args.Length > 1 && int.TryParse(args[1], out int c) ? c : 1000;

				// var seeder = host.Services.GetRequiredService<MainSeeder>();
				var seeder = new MainSeeder();

				switch (seedtype.ToLower())
				{
					case "all":
					default:
						seeder.SeedAllAsync(count);
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred during seeding: {ex.Message}");
				Environment.Exit(1);
			}
		}
	}
}
