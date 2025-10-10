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
				},
				new Profile
				{
					FullName = "Laura Chen",
					Bio = "Frontend developer passionate about design systems and user experience.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*laura-avatar.jpg",
				},
				new Profile
				{
					FullName = "Michael Rivera",
					Bio = "Tech journalist exploring the intersection of innovation, ethics, and society.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*michael-avatar.jpg",
				},
				new Profile
				{
					FullName = "Sofia Martins",
					Bio = "Software engineer and open-source advocate who writes about .NET and web technologies.",
					ProfilePic = "https://miro.medium.com/v2/resize:fit:200/1*sofia-avatar.jpg",
				},
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
					},
					new Article
					{
						Slug = "the-future-of-smart-homes",
						Title = "The Future of Smart Homes: Beyond Voice Assistants",
						Description = "From predictive lighting to emotion-aware devices — what’s next in home automation?",
						Tags = new List<string> { "Technology", "IoT", "Innovation" },
						CreatedAt = DateTime.UtcNow.AddDays(-30),
						UpdatedAt = DateTime.UtcNow.AddDays(-25),
						Favorited = true,
						FovoritesCount = 2450,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*smart-home.jpg",
						ClapsCount = 2400,
						CommentsCount = 189,
						Author = Authors[1],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "embracing-creative-failure",
						Title = "Embracing Creative Failure: Why Your Worst Ideas Matter",
						Description = "Behind every breakthrough lies a trail of failed experiments — and that’s okay.",
						Tags = new List<string> { "Creativity", "Psychology", "Productivity" },
						CreatedAt = DateTime.UtcNow.AddDays(-22),
						UpdatedAt = DateTime.UtcNow.AddDays(-20),
						Favorited = false,
						FovoritesCount = 1380,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*creative-failure.jpg",
						ClapsCount = 1410,
						CommentsCount = 82,
						Author = Authors[2],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "zero-to-hero-in-blazor",
						Title = "Zero to Hero in Blazor: Building Modern Web Apps in .NET",
						Description = "Discover how Blazor bridges the gap between client and server with C#.",
						Tags = new List<string> { "Blazor", ".NET", "Web Development" },
						CreatedAt = DateTime.UtcNow.AddDays(-12),
						UpdatedAt = DateTime.UtcNow.AddDays(-11),
						Favorited = true,
						FovoritesCount = 3230,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*blazor-guide.jpg",
						ClapsCount = 3250,
						CommentsCount = 205,
						Author = Authors[0],
						Publication = Publications[2]
					},
					new Article
					{
						Slug = "why-your-brain-loves-lists",
						Title = "Why Your Brain Loves Lists (And How to Use Them Wisely)",
						Description = "Lists aren’t just for groceries — they’re a psychological tool for focus and creativity.",
						Tags = new List<string> { "Psychology", "Habits", "Self-Improvement" },
						CreatedAt = DateTime.UtcNow.AddDays(-18),
						UpdatedAt = DateTime.UtcNow.AddDays(-17),
						Favorited = false,
						FovoritesCount = 970,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*brain-lists.jpg",
						ClapsCount = 995,
						CommentsCount = 64,
						Author = Authors[3],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "the-art-of-slow-productivity",
						Title = "The Art of Slow Productivity: Working Less to Create More",
						Description = "Rethinking hustle culture — why doing less can lead to deeper work.",
						Tags = new List<string> { "Work", "Mindfulness", "Productivity" },
						CreatedAt = DateTime.UtcNow.AddDays(-40),
						UpdatedAt = DateTime.UtcNow.AddDays(-35),
						Favorited = true,
						FovoritesCount = 4150,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*slow-productivity.jpg",
						ClapsCount = 4300,
						CommentsCount = 312,
						Author = Authors[4],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "gaming-as-storytelling",
						Title = "Gaming as Storytelling: Why Games Are the Literature of Our Time",
						Description = "Modern games aren’t just entertainment — they’re powerful storytelling mediums.",
						Tags = new List<string> { "Gaming", "Culture", "Storytelling" },
						CreatedAt = DateTime.UtcNow.AddDays(-16),
						UpdatedAt = DateTime.UtcNow.AddDays(-14),
						Favorited = true,
						FovoritesCount = 2860,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*gaming-story.jpg",
						ClapsCount = 2800,
						CommentsCount = 192,
						Author = Authors[2],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "designing-for-dark-mode",
						Title = "Designing for Dark Mode: More Than Just Inverted Colors",
						Description = "How contrast, perception, and accessibility shape dark UI design.",
						Tags = new List<string> { "Design", "UI", "Accessibility" },
						CreatedAt = DateTime.UtcNow.AddDays(-9),
						UpdatedAt = DateTime.UtcNow.AddDays(-8),
						Favorited = true,
						FovoritesCount = 1650,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*dark-mode-design.jpg",
						ClapsCount = 1680,
						CommentsCount = 74,
						Author = Authors[1],
						Publication = Publications[2]
					},
					new Article
					{
						Slug = "the-silent-power-of-routines",
						Title = "The Silent Power of Routines: How Habits Shape Creative Success",
						Description = "Creativity isn’t chaos — it’s built on consistent structure.",
						Tags = new List<string> { "Habits", "Creativity", "Self-Discipline" },
						CreatedAt = DateTime.UtcNow.AddDays(-7),
						UpdatedAt = DateTime.UtcNow.AddDays(-7),
						Favorited = false,
						FovoritesCount = 890,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*routines-power.jpg",
						ClapsCount = 900,
						CommentsCount = 51,
						Author = Authors[0],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "demystifying-async-await",
						Title = "Demystifying async/await: The Magic Behind Modern C#",
						Description = "A deep dive into asynchronous programming made simple.",
						Tags = new List<string> { "C#", "Programming", "Asynchronous" },
						CreatedAt = DateTime.UtcNow.AddDays(-14),
						UpdatedAt = DateTime.UtcNow.AddDays(-10),
						Favorited = true,
						FovoritesCount = 3780,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*async-await.jpg",
						ClapsCount = 3810,
						CommentsCount = 230,
						Author = Authors[4],
						Publication = Publications[2]
					},
					new Article
					{
						Slug = "ai-and-human-creativity",
						Title = "AI and Human Creativity: Collaboration, Not Competition",
						Description = "How artists and algorithms can inspire each other to create more.",
						Tags = new List<string> { "AI", "Art", "Creativity" },
						CreatedAt = DateTime.UtcNow.AddDays(-21),
						UpdatedAt = DateTime.UtcNow.AddDays(-18),
						Favorited = true,
						FovoritesCount = 5070,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*ai-creativity.jpg",
						ClapsCount = 5200,
						CommentsCount = 412,
						Author = Authors[3],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "building-a-minimalist-lifestyle",
						Title = "Building a Minimalist Lifestyle in a Digital World",
						Description = "Declutter your apps, feeds, and mind — minimalism for the information age.",
						Tags = new List<string> { "Lifestyle", "Minimalism", "Digital Detox" },
						CreatedAt = DateTime.UtcNow.AddDays(-60),
						UpdatedAt = DateTime.UtcNow.AddDays(-58),
						Favorited = false,
						FovoritesCount = 1540,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*digital-minimalism.jpg",
						ClapsCount = 1500,
						CommentsCount = 99,
						Author = Authors[1],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "the-rise-of-webassembly",
						Title = "The Rise of WebAssembly: A New Era for the Web",
						Description = "From Blazor to Rust — WebAssembly is redefining what’s possible in browsers.",
						Tags = new List<string> { "WebAssembly", "Blazor", "Web Development" },
						CreatedAt = DateTime.UtcNow.AddDays(-15),
						UpdatedAt = DateTime.UtcNow.AddDays(-13),
						Favorited = true,
						FovoritesCount = 3120,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*wasm-rise.jpg",
						ClapsCount = 3100,
						CommentsCount = 186,
						Author = Authors[0],
						Publication = Publications[2]
					},
					new Article
					{
						Slug = "the-mindful-developer",
						Title = "The Mindful Developer: Coding Without Burnout",
						Description = "How mindfulness techniques can transform your workflow and focus.",
						Tags = new List<string> { "Programming", "Mindfulness", "Work-Life Balance" },
						CreatedAt = DateTime.UtcNow.AddDays(-32),
						UpdatedAt = DateTime.UtcNow.AddDays(-31),
						Favorited = true,
						FovoritesCount = 2700,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*mindful-developer.jpg",
						ClapsCount = 2680,
						CommentsCount = 142,
						Author = Authors[2],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "why-typefaces-matter",
						Title = "Why Typefaces Matter: The Psychology of Fonts",
						Description = "Fonts shape emotion and meaning more than we realize.",
						Tags = new List<string> { "Design", "Typography", "Psychology" },
						CreatedAt = DateTime.UtcNow.AddDays(-50),
						UpdatedAt = DateTime.UtcNow.AddDays(-47),
						Favorited = false,
						FovoritesCount = 1150,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*typography-psych.jpg",
						ClapsCount = 1170,
						CommentsCount = 66,
						Author = Authors[1],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "hacking-your-learning-habits",
						Title = "Hacking Your Learning Habits: The Science of Skill Acquisition",
						Description = "Learn faster by aligning your brain’s reward system with your goals.",
						Tags = new List<string> { "Learning", "Neuroscience", "Productivity" },
						CreatedAt = DateTime.UtcNow.AddDays(-17),
						UpdatedAt = DateTime.UtcNow.AddDays(-15),
						Favorited = true,
						FovoritesCount = 3420,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*learning-hacks.jpg",
						ClapsCount = 3390,
						CommentsCount = 205,
						Author = Authors[4],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "the-ethics-of-algorithms",
						Title = "The Ethics of Algorithms: Who Decides What’s Fair?",
						Description = "Unpacking bias, transparency, and accountability in modern AI systems.",
						Tags = new List<string> { "AI", "Ethics", "Technology" },
						CreatedAt = DateTime.UtcNow.AddDays(-45),
						UpdatedAt = DateTime.UtcNow.AddDays(-43),
						Favorited = true,
						FovoritesCount = 3980,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*ai-ethics.jpg",
						ClapsCount = 4050,
						CommentsCount = 355,
						Author = Authors[3],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "rediscovering-email",
						Title = "Rediscovering Email: The Old Tool That Still Works",
						Description = "Why newsletters and thoughtful emails are making a comeback.",
						Tags = new List<string> { "Communication", "Productivity", "Writing" },
						CreatedAt = DateTime.UtcNow.AddDays(-26),
						UpdatedAt = DateTime.UtcNow.AddDays(-25),
						Favorited = false,
						FovoritesCount = 840,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*email-renaissance.jpg",
						ClapsCount = 810,
						CommentsCount = 43,
						Author = Authors[1],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "beyond-the-todo-list",
						Title = "Beyond the To-Do List: Systems Thinking for Personal Productivity",
						Description = "Stop managing tasks — start managing your inputs and outcomes.",
						Tags = new List<string> { "Productivity", "Systems", "Focus" },
						CreatedAt = DateTime.UtcNow.AddDays(-13),
						UpdatedAt = DateTime.UtcNow.AddDays(-12),
						Favorited = true,
						FovoritesCount = 2880,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*todolist-systems.jpg",
						ClapsCount = 2900,
						CommentsCount = 190,
						Author = Authors[2],
						Publication = Publications[2]
					},
					new Article
					{
						Slug = "small-teams-big-impact",
						Title = "Small Teams, Big Impact: The Power of Focused Collaboration",
						Description = "How tight-knit teams outperform massive organizations.",
						Tags = new List<string> { "Business", "Teams", "Leadership" },
						CreatedAt = DateTime.UtcNow.AddDays(-38),
						UpdatedAt = DateTime.UtcNow.AddDays(-37),
						Favorited = false,
						FovoritesCount = 1950,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*small-teams.jpg",
						ClapsCount = 1900,
						CommentsCount = 128,
						Author = Authors[4],
						Publication = Publications[1]
					},
					new Article
					{
						Slug = "breaking-the-scroll-loop",
						Title = "Breaking the Scroll Loop: Designing for Digital Wellbeing",
						Description = "The UI tricks that keep us hooked — and how to design against them.",
						Tags = new List<string> { "UX", "Design", "Wellbeing" },
						CreatedAt = DateTime.UtcNow.AddDays(-20),
						UpdatedAt = DateTime.UtcNow.AddDays(-18),
						Favorited = true,
						FovoritesCount = 3620,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*scroll-loop.jpg",
						ClapsCount = 3650,
						CommentsCount = 277,
						Author = Authors[0],
						Publication = Publications[0]
					},
					new Article
					{
						Slug = "mastering-focus-in-a-distracted-world",
						Title = "Mastering Focus in a Distracted World",
						Description = "Deep work is a competitive advantage — here’s how to reclaim it.",
						Tags = new List<string> { "Focus", "Work", "Psychology" },
						CreatedAt = DateTime.UtcNow.AddDays(-28),
						UpdatedAt = DateTime.UtcNow.AddDays(-27),
						Favorited = true,
						FovoritesCount = 4100,
						Image = "https://miro.medium.com/v2/resize:fit:800/1*deep-focus.jpg",
						ClapsCount = 4050,
						CommentsCount = 299,
						Author = Authors[3],
						Publication = Publications[2]
					}
				);

				context.SaveChanges();
			}
		}
	}
}
