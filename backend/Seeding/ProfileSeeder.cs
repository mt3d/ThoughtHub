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
			await SeedFixedProfile();
			await SeedRandomProfiles(count - 1);
		}

		public async Task SeedFixedProfile()
		{
			var faker = new Faker();
			var fullName = "Mark Jameson";
			var user = new User
			{
				UserName = "mark_jameson",
				Email = "mark_jameson@example.com",
				EmailConfirmed = true
			};
			var result = await _userManager.CreateAsync(user, "Password123!");

			var profile = new Profile()
			{
				UserId = user.Id,
				FullName = fullName,
				Bio = GenerateBio(faker),
				ProfilePictureId = await _imageCreator.CreateProfileImageAsync($"{user.UserName}_profile_pic.png")
			};

			_context.Profiles.Add(profile);
			await _context.SaveChangesAsync();
		}

		public async Task SeedRandomProfiles(int count)
		{
			var faker = new Faker();

			for (int i = 0; i < count; i++)
			{
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

				var profile = new Profile()
				{
					UserId = user.Id,
					FullName = fullName,
					Bio = GenerateBio(faker),
					ProfilePictureId = await _imageCreator.CreateProfileImageAsync($"{user.UserName}_profile_pic.png")
				};

				_context.Profiles.Add(profile);
			}

			await _context.SaveChangesAsync();
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
