namespace ThoughtHub.Seeding
{
	public class MainSeeder
	{
		private readonly ProfileSeeder _profileSeeder;
		private readonly TagSeeder _tagSeeder;

		public MainSeeder(
			ProfileSeeder profileSeeder,
			TagSeeder tagSeeder)
		{
			_profileSeeder = profileSeeder;
			_tagSeeder = tagSeeder;
		}

		public async Task SeedAllAsync(int articleCount = 1000)
		{
			Console.WriteLine("Starting complete database seeding...");

			await _profileSeeder.SeedAsync(10);
			await _tagSeeder.SeedAsync();

			Console.WriteLine("Seeding completed.");
		}
	}
}
