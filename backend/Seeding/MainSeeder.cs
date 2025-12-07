namespace ThoughtHub.Seeding
{
	public class MainSeeder
	{
		private readonly ProfileSeeder _profileSeeder;
		private readonly TagSeeder _tagSeeder;
		private readonly PublicationSeeder _publicationSeeder;

		public MainSeeder(
			ProfileSeeder profileSeeder,
			TagSeeder tagSeeder,
			PublicationSeeder publicationSeeder)
		{
			_profileSeeder = profileSeeder;
			_tagSeeder = tagSeeder;
			_publicationSeeder = publicationSeeder;
		}

		public async Task SeedAllAsync(int articleCount = 1000)
		{
			Console.WriteLine("Starting complete database seeding...");

			await _profileSeeder.SeedAsync(10);
			await _tagSeeder.SeedAsync();
			await _publicationSeeder.SeedAsync(20);

			Console.WriteLine("Seeding completed.");
		}
	}
}
