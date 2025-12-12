using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data;

namespace ThoughtHub.Services
{
	public class ArticleService : IArticleService
	{
		private readonly IArticleRepository _repo;

		public ArticleService(IArticleRepository repo)
		{
			_repo = repo;
		}

		public async Task<ArticleM?> GetByIdAsync(Guid id)
		{
			// TODO: Check the cache

			var model = await _repo.GetById(id).ConfigureAwait(false);

			if (model is not null)
			{
				// TODO: Format the perma link (based on the publication and the author)

				// TODO: Init primary image and og image

				// TODO: Cache the model

				return model;
			}

			return null;
		}

		public Task SaveAsync(ArticleM model)
		{
			return SaveAsync(model, false);
		}

		private async Task SaveAsync(ArticleM model, bool isDraft)
		{
			if (model.Id == Guid.Empty)
			{
				model.Id = Guid.NewGuid();
			}

			// TODO: Validate the model
			// TODO: Valdidate the title field

			if (string.IsNullOrEmpty(model.Slug))
			{
				model.Slug = Utils.GenerateSlug(model.Title, false);
			}
			else
			{
				model.Slug = Utils.GenerateSlug(model.Title, false);
			}

			// TODO: Validate that slug is not null or empty

			// TODO: Ensure the slug is unique

			// TODO: Get the current post 
			if (isDraft) // TODO: Check if it is published or scheduled
			{

			}
			else // drafts for unpublished articles
			{
				// TODO: Check if current is null and isDraft

				// TODO: Add a new revision

				await _repo.SaveAsync(model).ConfigureAwait(false);
			}

			// TODO: Update search document

			// TODO: Remove the post from the cache (if updated)
		}
	}
}
