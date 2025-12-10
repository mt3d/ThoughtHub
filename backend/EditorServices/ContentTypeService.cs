using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Services;

namespace ThoughtHub.EditorServices
{
	public class ContentTypeService
	{
		private readonly BlocksRegistry _registry;

		public ContentTypeService(BlocksRegistry registry)
		{
			_registry = registry;
		}

		public async Task<BlockModel> CreateBlockAsync(string type)
		{
			throw new NotImplementedException();
		}

		public BlockListModel GetArticleBlockTypes(string? parentType = null)
		{
			var model = new BlockListModel();
			// TODO: Handle parent

			foreach (var category in _registry.GetCategories().OrderBy(c => c))
			{
				var listCategory = new BlockListModel.BlockListCategory
				{
					Name = category
				};

				var items = _registry.GetByCategory(category).OrderBy(i => i.Name).ToList();

				// TODO: Handle parent relationships

				foreach (var block in items)
				{
					listCategory.Items.Add(new BlockListModel.BlockListItem
					{
						Name = block.Name,
						Icon = block.Icon,
						Type = block.Type
					});
				}

				model.Categories.Add(listCategory);
			}

			// Remove empty categories
			var empty = model.Categories.Where(c => c.Items.Count() == 0).ToList();
			foreach (var toRemove in empty)
			{
				model.Categories.Remove(toRemove);
			}

			model.TypeCount = model.Categories.Sum(c => c.Items.Count);

			return model;
		}
	}
}
