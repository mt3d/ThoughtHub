namespace ThoughtHub.Seeding
{
	public class MainSeeder
	{
		private readonly ProfileSeeder _profileSeeder;
		private readonly TagSeeder _tagSeeder;
		private readonly PublicationSeeder _publicationSeeder;
		private readonly ArticleSeeder _articleSeeder;

		public MainSeeder(
			ProfileSeeder profileSeeder,
			TagSeeder tagSeeder,
			PublicationSeeder publicationSeeder,
			ArticleSeeder articleSeeder)
		{
			_profileSeeder = profileSeeder;
			_tagSeeder = tagSeeder;
			_publicationSeeder = publicationSeeder;
			_articleSeeder = articleSeeder;
		}

		public async Task SeedAllAsync(int articleCount = 1000)
		{
			Console.WriteLine("Starting complete database seeding...");

			await _profileSeeder.SeedAsync(10);
			await _tagSeeder.SeedAsync();
			await _publicationSeeder.SeedAsync(20);
			await _articleSeeder.SeedAsync(30);

			Console.WriteLine("Seeding completed.");
		}
	}
}
