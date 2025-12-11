using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.Runtime;
using ThoughtHub.Services;

namespace ThoughtHub.EditorServices
{
	public class ContentTypeService
	{
		private readonly BlocksRegistry _registry;
		private readonly IContentFactory _contentFactory;

		public ContentTypeService(BlocksRegistry registry, IContentFactory contentFactory)
		{
			_registry = registry;
			_contentFactory = contentFactory;
		}

		public async Task<BlockEditModel?> CreateBlockAsync(string type)
		{
			var blockTypeDescriptor = _registry.GetByTypeName(type);

			if (blockTypeDescriptor != null)
			{
				var block = (BlockModel?)(await _contentFactory.CreateBlockAsync(type));

				// TODO: Handle block groups in the future if necessary.

				if (block is not null)
				{
					return new SingleBlockEditModel
					{
						Block = block,
						Name = blockTypeDescriptor.Name,
						// TODO: Return title
						Icon = blockTypeDescriptor.Icon,
						Component = blockTypeDescriptor.Component,
					};
				}

				// TODO: Handle generic blocks in the future if necessary.
			}

			return null;
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

				var blockDescriptors = _registry.GetByCategory(category).OrderBy(i => i.Name).ToList();

				// TODO: Handle parent relationships

				foreach (var descriptor in blockDescriptors)
				{
					listCategory.Items.Add(new BlockListModel.BlockListItem
					{
						Name = descriptor.Name,
						Icon = descriptor.Icon,
						Type = descriptor.TypeName
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
