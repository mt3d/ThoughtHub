using Bogus;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Seeding
{
	public class TagSeeder
	{
		private readonly PlatformContext _context;
		
		private string[] _stringTags = new[]
		{
			"ChatGPT",
			"Productivity",
			"UX",
			"Design",
			"Psychology",
			"Focus",
			"Work",
			"Wellbeing",
			"Business",
			"Teams",
			"Leadership",
			"Systems",
			"Communication",
			"Writing",
			"Ethics",
			"Technology",
			"Learning",
			"Neuroscience",
			"Typography",
			"Programming",
			"Mindfulness",
			"Work-Life Balance",
			"WebAssembly",
			"Blazor",
			"Web Development",
			"Minimalism",
			"Digital Detox",
			"Lifestyle",
			"Art",
			"Creativity",
			"C#",
			"Asynchronous",
			"Habits",
			"Self-Discipline",
			"UI",
			"Accessibility",
			".NET",
			"Self-Improvement",
			"Storytelling",
			"Culture",
			"Gaming",
			"Startups",
			"Entrepreneurship",
			"IoT",
			"Innovation"
		};

		public TagSeeder(PlatformContext context)
		{
			_context = context;
		}

		public async Task SeedAsync()
		{
			if (await _context.Tags.AnyAsync())
			{
				Console.WriteLine("Tags already exists. Skipping tag seeding.");
				return;
			}

			var tags = new List<Tag>();

			foreach (var tag in _stringTags)
			{
				tags.Add(new Tag { Name = tag });
			}

			await _context.Tags.AddRangeAsync(tags);
			await _context.SaveChangesAsync();
		}
	}
}
