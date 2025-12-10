using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Services
{
	public class BlocksRegistry
	{
		//protected readonly List<BlockModel> _blocks = new List<BlockModel>();
		protected readonly Dictionary<string, BlockModel> _blocksDictionary = new Dictionary<string, BlockModel>();

		public void Register<TBlockChild>() where TBlockChild : BlockModel
		{
			var type = typeof(TBlockChild);
			var item = Activator.CreateInstance<TBlockChild>();

			// TODO: Handle duplicate keys.
			_blocksDictionary.Add(type.ToString(), item);
		}

		public IEnumerable<string> GetCategories()
		{
			return _blocksDictionary.Values
				.Select(b => b.Category)
				.Distinct()
				.OrderBy(c => c)
				.ToArray();
		}

		public IEnumerable<BlockModel> GetByCategory(string category)
		{
			var query = _blocksDictionary.Values.Where(i => i.Category == category);

			return query.ToArray();
		}
	}
}
