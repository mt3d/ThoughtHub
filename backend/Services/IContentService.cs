using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IContentService
	{
		ArticleM TransformArticleEntityIntoModel(Article article);

		IList<Block> TransformBlocks(IList<BlockModel> models);

		IList<BlockModel> TransformBlockEntitiesIntoModels(IEnumerable<Block> blocks);
	}
}
