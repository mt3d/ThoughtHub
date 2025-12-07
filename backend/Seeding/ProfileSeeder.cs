using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Identity;

namespace ThoughtHub.Seeding
{
	public class ProfileSeeder
	{
		private readonly PlatformContext _context;
		private readonly UserManager<User> _userManager;
		private readonly ImageCreator _imageCreator;

		public ProfileSeeder(
			PlatformContext context,
			UserManager<User> userManager,
			ImageCreator imageCreator)
		{
			_context = context;
			_userManager = userManager;
			_imageCreator = imageCreator;
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

			foreach (var user in users)
			{
				var profile = await GenerateProfileAsync(faker, user);
				profiles.Add(profile);
			}

			await _context.Profiles.AddRangeAsync(profiles);
			await _context.SaveChangesAsync();
		}

		private async Task EnsureUsersExist(int count)
		{
			// TODO: Move inside te profile creation loop.
			for (int i = 0; i < count; i++)
			{
				var faker = new Faker();

				var fullName = faker.Name.FullName();

				var user = new User
				{
					UserName = faker.Internet.UserName(fullName),
					Email = faker.Internet.Email(fullName),
					EmailConfirmed = true
				};

				/**
				 * Important: When you seed users manually using UserStore (instead of
				 * using UserManager.CreateAsync), Identity doesn’t automatically fill
				 * normalized fields for you.
				 */
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
				Bio = GenerateBio(faker),
				ProfilePictureId = await _imageCreator.CreateProfileImageAsync($"{user.UserName}_profile_pic.png")
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
