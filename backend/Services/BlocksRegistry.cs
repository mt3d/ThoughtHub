using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public class BlocksRegistry
	{
		protected readonly List<BlockModel> _blocks = new List<BlockModel>();

		public void Register<TBlockChild>() where TBlockChild : BlockModel
		{

		}

		public IEnumerable<string> GetCategories()
		{
			return _blocks
				.Select(b => b.Category)
				.Distinct()
				.OrderBy(c => c)
				.ToArray();
		}

		public IEnumerable<BlockModel> GetByCategory(string category)
		{
			var query = _blocks.Where(i => i.Category == category);

			return query.ToArray();
		}
	}
}
