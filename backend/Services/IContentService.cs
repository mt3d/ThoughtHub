using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public interface IContentService
	{
		IList<Block> TransformBlocks(IList<BlockModel> models);
	}
}
