using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Services;

namespace ThoughtHub.Data
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly PlatformContext _context;
		private readonly IContentService _contentService;

		public ArticleRepository(
			PlatformContext context,
			IContentService contentService)
		{
			_context = context;
			_contentService = contentService;
		}

		public async Task<ArticleM?> GetById(Guid id)
		{
			var article = await _context.Articles
				.AsNoTracking()
				.Include(a => a.Blocks).ThenInclude(b => b.Block)
				// TODO: Include tags
				.OrderBy(a => a.CreatedAt)
				.FirstOrDefaultAsync(a => a.Id == id);

			if (article != null)
			{
				return _contentService.TransformArticleEntityIntoModel(article);
			}

			return null;
		}

		public Task SaveAsync(ArticleM model)
		{
			return SaveAsync(model, false);
		}

		private async Task SaveAsync(ArticleM model, bool isDraft)
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
				.Include(p => p.Blocks).ThenInclude(b => b.Block).ThenInclude(b => b.Fields)
				.FirstOrDefaultAsync(a => a.Id == model.Id)
				.ConfigureAwait(false);

			if (article == null)
			{
				article = new Article
				{
					Id = model.Id != Guid.Empty ? model.Id : Guid.NewGuid(),
					CreatedAt = DateTime.Now, // TODO: UtcNow?
					UpdatedAt = DateTime.Now
				};
				model.Id = article.Id;

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
			var blockModels = model.BlockModels;

			if (blockModels != null)
			{
				var blocks = _contentService.TransformBlocks(blockModels);

				// a list of block IDs that the editor sent in the new model
				// Anything not in current is considered removed by the user.
				var current = blocks.Select(b => b.Id).ToArray();

				var removed = article.Blocks
					.Where(b => !current.Contains(b.BlockId))
					.Select(b => b.Block);
				// TODO: Remove child blocks in the future.

				if (!isDraft)
				{
					_context.Blocks.RemoveRange(removed);
				}

				article.Blocks.Clear();

				for (int i = 0; i < blocks.Count; i++)
				{
					var block = AddBlock(blocks[i], isDraft);

					var articleBlock = new ArticleBlock
					{
						Id = Guid.NewGuid(),
						BlockId = block.Id,
						Block = block,
						ArticleId = article.Id,
						SortOrder = i
					};

					if (!isDraft)
					{
						_context.ArticleBlocks.Add(articleBlock);
					}

					article.Blocks.Add(articleBlock);
				}
			}

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
					.FirstOrDefaultAsync(r => r.ArticleId == article.Id && r.CreatedAt > DateTime.MinValue)
					.ConfigureAwait(false);

				if (draft == null)
				{
					draft = new ArticleRevision
					{
						Id = Guid.NewGuid(),
						ArticleId = article.Id
					};

					await _context.ArticleRevisions.AddAsync(draft).ConfigureAwait(false);
				}

				draft.Data = JsonSerializer.Serialize(article);
				draft.CreatedAt = article.UpdatedAt;

				await _context.SaveChangesAsync().ConfigureAwait(false);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// This method doesn't commit the returned block to the database.
		/// It just adds to the DbContext.
		/// </remarks>
		private Block AddBlock(Block blockToSave, bool isDraft)
		{
			IQueryable<Block> blockQuery = _context.Blocks;
			if (isDraft)
			{
				blockQuery = blockQuery.AsNoTracking();
			}

			var block = blockQuery
				.Include(b => b.Fields)
				.FirstOrDefault(b => b.Id == blockToSave.Id);

			if (block == null)
			{
				block = new Block()
				{
					Id = blockToSave.Id != Guid.Empty ? blockToSave.Id : Guid.NewGuid(),
					CreatedAt = DateTime.Now
				};

				if (!isDraft)
				{
					_context.Blocks.Add(block);
				}
			}
			block.ClrType = blockToSave.ClrType;
			block.UpdatedAt = blockToSave.UpdatedAt;

			UpdateFields(blockToSave, block, isDraft);

			return block;
		}

		private void UpdateFields(Block newBlock, Block currentBlock, bool isDraft)
		{
			var currentFieldIds = newBlock.Fields.Select(f => f.FieldId).Distinct(); // TODO: Why distinct?
			var removedFields = currentBlock.Fields.Where(f => !currentFieldIds.Contains(f.FieldId));

			if (!isDraft)
			{
				_context.BlockFields.RemoveRange(removedFields);
			}

			foreach (var newField in newBlock.Fields)
			{
				var field = currentBlock.Fields.FirstOrDefault(f => f.FieldId == newField.FieldId);

				if (field == null)
				{
					field = new BlockField()
					{
						Id = newField.Id != Guid.Empty ? newField.Id : Guid.NewGuid(),
						BlockId = currentBlock.Id,
						FieldId = newField.FieldId
					};

					if (!isDraft)
					{
						_context.BlockFields.Add(field);
					}
					currentBlock.Fields.Add(field);
				}
				field.SortOrder = newField.SortOrder;
				field.ClrType = newField.ClrType;
				field.SerializedValue = newField.SerializedValue;
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
