using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;
using ThoughtHub.Api.Core.Entities;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Data
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly PlatformContext _context;

		public ArticleRepository(PlatformContext context)
		{
			_context = context;
		}

		public Task Save(ArticleM model)
		{
			return Save(model, false);
		}

		private async Task Save(ArticleM model, bool isDraft)
		{
			// Handle tags
			foreach (var tag in model.Tags)
			{

			}

			if (string.IsNullOrEmpty(model.Slug))
			{
				model.Slug = Utils.GenerateSlug(model.Title, false);
			}
			else
			{
				model.Slug = Utils.GenerateSlug(model.Slug, false);
			}

			// TODO: Explain.
			IQueryable<Article> articleQuery = _context.Articles;
			if (isDraft)
			{
				articleQuery = articleQuery.AsNoTracking();
			}

			var article = await articleQuery
				// TODO: Include blocks, fields, and tags
				.FirstOrDefaultAsync(a => a.ArticleId == model.Id)
				.ConfigureAwait(false);

			if (article == null)
			{
				article = new Article
				{
					ArticleId = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
					CreatedAt = DateTime.Now, // TODO: UtcNow?
					UpdatedAt = DateTime.Now
				};
				model.Id = article.ArticleId;

				if (!isDraft)
				{
					_context.Articles.Add(article);
				}
			}
			else
			{
				article.UpdatedAt = DateTime.Now;
			}

			// TODO: Transform model to article

			// TODO: Set key for fields

			// TODO: Transform blocks

			// TODO: Remove the old tags
			// TODO: Add the new tags

			// Save the article
			if (!isDraft)
			{
				await _context.SaveChangesAsync().ConfigureAwait(false);
				// TODO: Delete unused tags from the database
			}
			else
			{
				var draft = await _context.ArticleRevisions
					// TODO: Explain
					.FirstOrDefaultAsync(r => r.ArticleId == article.ArticleId && r.CreatedAt > DateTime.MinValue)
					.ConfigureAwait(false);

				if (draft == null)
				{
					draft = new ArticleRevision
					{
						Id = Guid.NewGuid(),
						ArticleId = article.ArticleId
					};

					await _context.ArticleRevisions.AddAsync(draft).ConfigureAwait(false);
				}

				draft.Data = JsonSerializer.Serialize(article);
				draft.CreatedAt = article.UpdatedAt;

				await _context.SaveChangesAsync().ConfigureAwait(false);
			}
		}
	}

	public static class Utils
	{
		// TODO: Initial implementation. More work is needed.
		// TODO: Should replace the one used by the seeder.
		public static string GenerateSlug(string str, bool hierarchical = true)
		{
			var slug = str.Trim().ToLower();

			slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

			// Replace spaces with dash
			slug = Regex.Replace(slug, @"\s+", "-");

			// Collapse multiple dashes
			slug = Regex.Replace(slug, @"-+", "-");

			return slug;
		}
	}
}
