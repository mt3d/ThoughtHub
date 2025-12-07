using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtHub.Tools.DataSeeder
{
	public class MainSeeder
	{
		//private readonly ProfileSeeder _profileSeeder;
		//private readonly ImageSeeder _imageSeeder;
		private readonly IImageGenerator _imageGenerator;

		public MainSeeder(
			//ProfileSeeder profileSeeder,
			//ImageSeeder imageSeeder,
			IImageGenerator imageGenerator)
		{
			//_profileSeeder = profileSeeder;
			//_imageSeeder = imageSeeder;
			_imageGenerator = imageGenerator;
		}

		public async Task SeedAllAsync(int articleCount = 1000)
		{
			Console.WriteLine("Starting complete database seeding...");

			_imageGenerator.GenerateImages(100);

			//await _imageSeeder.SeedImages();

			Console.WriteLine("Seeding completed.");
		}

		public async Task SeedImagesAsync()
		{
			Console.WriteLine("Seeding sample images...");
			//await _imageSeeder.SeedImages();
		}
	}
}
