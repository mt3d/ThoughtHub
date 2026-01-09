using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data;

namespace ThoughtHub.Services
{
	public class ArticleService : IArticleService
	{
		private readonly IArticleRepository _repo;
		private readonly PlatformContext _context;
		private readonly IMapper _mapper;
		private readonly IContentService _contentService;

		public ArticleService(
			IArticleRepository repo,
			PlatformContext context,
			IMapper mapper,
			IContentService contentService)
		{
			_repo = repo;
			_context = context;
			_mapper = mapper;
			_contentService = contentService;
		}

		private async Task<ArticleModel?> LoadAndTransformArticleAsync(Expression<Func<Article, bool>> predicate)
		{
			// TODO: Use the article repo.
			// TODO: Use caching.

			var article = await _context.Articles.AsNoTracking()
				.Include(a => a.Tags)
				.Include(a => a.Publication)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.User)
				.Include(a => a.AuthorProfile).ThenInclude(p => p.ProfilePicture)
				.Include(a => a.ArticleImage)
				.Include(a => a.Blocks).ThenInclude(b => b.Block).ThenInclude(b => b.Fields)
				.FirstOrDefaultAsync(predicate);

			if (article is not null)
			{
				return _contentService.TransformArticleEntityIntoModel(article);
			}

			return null;
		}

		public Task<ArticleModel?> GetIndependentArticleAsync(string userName, string slug)
		{
			return LoadAndTransformArticleAsync(a =>
				a.PublicationId == null
				&& a.AuthorProfile.User.UserName == userName
				&& a.Slug == slug);
		}

		public Task<ArticleModel?> GetPublicationArticleAsync(string publicationName, string slug)
		{
			return LoadAndTransformArticleAsync(a =>
				a.PublicationId != null
				&& a.Publication.Slug == publicationName
				&& a.Slug == slug);
		}

		public async Task<ArticleModel?> GetByIdAsync(Guid id)
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

		public Task SaveAsync(ArticleModel model)
		{
			return SaveAsync(model, false);
		}

		private async Task SaveAsync(ArticleModel model, bool isDraft)
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
