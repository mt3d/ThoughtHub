using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Runtime;
using ThoughtHub.Services;

namespace ThoughtHub.EditorServices
{
	public class ArticleService
	{
		private readonly Services.IArticleService _service;
		private readonly BlocksRegistry _blocksRegistry;

		public ArticleService(Services.IArticleService service)
		{
			_service = service;
		}

		public async Task<ArticleEditModel?> GetById(Guid id, bool lookForDrafts = true)
		{
			// TODO: Look for both drafts and published depending on the optional parameter.
			var article = await _service.GetByIdAsync(id);

			if (article is not null)
			{
				var editModel = new ArticleEditModel()
				{
					Id = article.Id,
					Title = article.Title,
					Slug = article.Slug,
				};

				foreach (var blockModel in article.BlockModels)
				{
					var blockTypeDescriptor = _blocksRegistry.GetByTypeName(blockModel.Type);

					editModel.Blocks.Add(new BlockEditModel
					{
						Block = blockModel,
						Name = blockTypeDescriptor.Name,
						Icon = blockTypeDescriptor.Icon,
						Component = blockTypeDescriptor.Component
					});
				}

				return editModel;
			}

			return null;
		}

		// create or update
		public async Task Save(ArticleEditModel model, bool isDraft)
		{
			if (model.Id == Guid.Empty)
			{
				model.Id = Guid.NewGuid();
			}

			var article = await _service.GetByIdAsync(model.Id);

			if (article is null)
			{
				article = new ArticleM();
				article.Id = model.Id;
			}

			article.Title = model.Title;
			article.Slug = model.Slug;

			// TODO: Save tags

			// Handle blocks
			article.BlockModels.Clear();

			foreach (var block in model.Blocks)
			{
				article.BlockModels.Add(block.Block);
			}

			if (isDraft)
			{
				// TODO: Implement SaveDraftAsync()
			}
			else
			{
				await _service.SaveAsync(article);
			}
		}
	}
}
