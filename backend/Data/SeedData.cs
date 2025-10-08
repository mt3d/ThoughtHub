using backend.Data.Entities;
using backend.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
	public static class SeedData
	{
		public static void EnsurePopulated(PlatformContext context)
		{
			//PlatformContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PlatformContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			List<Profile> Authors = new() {
				new Profile
				{
					//Username = "jwilkins",
					FullName = "James Wilkins",
					Bio = "Data scientist and writer about AI and productivity.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*james-avatar.jpg",
					//Following = false,
					//Url = "/@jwilkins"
				},
				new Profile
				{
					//Username = "sarahd",
					FullName = "Sarah Davis",
					Bio = "UX designer and writer about design psychology.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*sarah-avatar.jpg",
					//Following = true,
					//Url = "/@sarahd"
				},
				new Profile
				{
					//Username = "mkhan",
					FullName = "Mohammed Khan",
					Bio = "Founder, product builder, and storyteller.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*mkhan-avatar.jpg",
					//Following = false,
					//Url = "/@mkhan"
				}
			};

			List<Publication> Publications = new()
			{
				new Publication
				{
					Name = "Data Science Collective",
					Url = "/data-science-collective",
					Image = "https://miro.medium.com/v2/resize:fit:40/1*dsc-logo.jpg"
				},
				new Publication
				{
					Name = "UX Collective",
					Url = "/ux-collective",
					Image = "https://miro.medium.com/v2/resize:fit:40/1*uxc-logo.jpg"
				},
				new Publication
				{
					Name = "Startup Stories",
					Url = "/startup-stories",
					Image = "https://miro.medium.com/v2/resize:fit:40/1*startup-logo.jpg"
				}
			};

			if (!context.Articles.Any())
			{
				context.Articles.AddRange(
					new Article
					{
						Slug = "prompt-like-a-pro",
						Title = "You’re Using ChatGPT Wrong: How to Prompt Like a Pro",
						Description = "Smarter prompts lead to smarter responses.",
						Tags = new List<string> { "AI", "ChatGPT", "Productivity" },
						CreatedAt = DateTime.UtcNow.AddDays(-10),
						UpdatedAt = DateTime.UtcNow.AddDays(-5),
						Favorited = true,
						FovoritesCount = 6900,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*chatgpt-prompt.jpg",
						ClapsCount = 6900,
						CommentsCount = 368,
						Author = Authors[0],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "design-for-humans",
						Title = "Designing for Humans: The Secret to Great UX",
						Description = "Practical lessons for crafting user-centered products.",
						Tags = new List<string> { "UX", "Design", "Psychology" },
						CreatedAt = DateTime.UtcNow.AddDays(-30),
						UpdatedAt = DateTime.UtcNow.AddDays(-20),
						Favorited = false,
						FovoritesCount = 1200,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*ux-design.jpg",
						ClapsCount = 1200,
						CommentsCount = 87,
						Author = Authors[1],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "startup-lessons",
						Title = "10 Hard Lessons from My First Startup",
						Description = "What I wish I knew before launching.",
						Tags = new List<string> { "Startups", "Entrepreneurship", "Business" },
						CreatedAt = DateTime.UtcNow.AddDays(-60),
						UpdatedAt = DateTime.UtcNow.AddDays(-45),
						Favorited = true,
						FovoritesCount = 3400,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*startup.jpg",
						ClapsCount = 3400,
						CommentsCount = 152,
						Author = Authors[2],
						Publication = Publications[2]
					}
				);

				context.SaveChanges();
			}
		}
	}
}
