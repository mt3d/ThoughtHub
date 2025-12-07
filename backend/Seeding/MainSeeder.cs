namespace ThoughtHub.Seeding
{
	public class MainSeeder
	{
		private readonly ProfileSeeder _profileSeeder;
		//private readonly ImageSeeder _imageSeeder;
		//private readonly IImageGenerator _imageGenerator;

		public MainSeeder(
			ProfileSeeder profileSeeder)
			//ImageSeeder imageSeeder,
			//IImageGenerator imageGenerator)
		{
			_profileSeeder = profileSeeder;
		}

		public async Task SeedAllAsync(int articleCount = 1000)
		{
			Console.WriteLine("Starting complete database seeding...");

			await _profileSeeder.SeedAsync(10);

			Console.WriteLine("Seeding completed.");
		}
	}
}
