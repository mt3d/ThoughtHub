using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Services;

namespace ThoughtHub.EditorServices
{
	public class ArticleService
	{
		public Services.IArticleService _service;

		public ArticleService(Services.IArticleService service)
		{
			_service = service;
		}

		public async Task<ArticleEditModel> GetById(Guid id)
		{
			throw new NotImplementedException();
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
