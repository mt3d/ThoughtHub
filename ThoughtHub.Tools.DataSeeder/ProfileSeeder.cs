using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Identity;

namespace ThoughtHub.Tools.DataSeeder
{
	public class ProfileSeeder
	{
		private readonly PlatformContext _context;
		private readonly UserManager<User> _userManager;

		public ProfileSeeder(
			PlatformContext context,
			UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task SeedAsync(int count)
		{
			if (await _context.Profiles.AnyAsync())
			{
				Console.WriteLine("Users already exists. Skipping user seeding.");
				return;
			}

			await EnsureUsersExist(count);

			var users = await _userManager.Users.ToListAsync();
			var profiles = new List<Profile>();
			var faker = new Faker();

			foreach (var user in users.Take(count)) // added flexibility
			{
				var profile = await GenerateProfileAsync(faker, user);
				profiles.Add(profile);
			}

			await _context.Profiles.AddRangeAsync(profiles);
			await _context.SaveChangesAsync();
		}

		private async Task EnsureUsersExist(int count)
		{
			var existingCount = await _userManager.Users.CountAsync();

			if (existingCount >= count)
			{
				Console.WriteLine($"Found {existingCount} existing users.");
				return;
			}

			var usersToCreate = count - existingCount;
			Console.WriteLine($"Creating {usersToCreate} users.");

			for (int i = 0; i < usersToCreate; i++)
			{
				var faker = new Faker();

				var fullName = faker.Name.FullName();

				var user = new User
				{
					UserName = faker.Internet.UserName(fullName),
					Email = faker.Internet.Email(fullName),
					EmailConfirmed = true
				};

				var result = await _userManager.CreateAsync(user, "Password123!");

				if (!result.Succeeded)
				{
					Console.WriteLine($"Failed to create user: {string.Join(", ", result.Errors)}");
				}
			}
		}

		private async Task<Profile> GenerateProfileAsync(Faker faker, User user)
		{
			var fullName = faker.Name.FullName();

			var profile = new Profile()
			{
				UserId = user.Id,
				FullName = fullName,
				Bio = GenerateBio(faker)
			};

			return profile;
		}

		private string GenerateBio(Faker faker)
		{
			var bios = new[]
			{
				$"{faker.Name.JobTitle()} at {faker.Company.CompanyName()}. {faker.Lorem.Sentence}"
			};

			return faker.PickRandom(bios);
		}
	}
}
