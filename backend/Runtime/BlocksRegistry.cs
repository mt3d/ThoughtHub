using System.Reflection;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Runtime
{
	/// <summary>
	/// A registry of all the available block types.
	/// </summary>
	public class BlocksRegistry
	{
		/// <summary>
		/// We store a BlockTypeDescriptor not BlockModel. Each instance of BlockModel
		/// represents a single block. This is however is a registry of the available blocks,
		/// and that's why we need a dedicated TypeDescriptor.
		/// </summary>
		private readonly List<BlockTypeDescriptor> _blockDescriptors = new List<BlockTypeDescriptor>();

		public void Register<TBlockChild>() where TBlockChild : BlockModel
		{
			var type = typeof(TBlockChild);
			var item = Activator.CreateInstance<TBlockChild>();

			if (_blockDescriptors.Where(b => b.Type == type).Count() == 0)
			{
				BlockTypeDescriptor descriptor = new BlockTypeDescriptor();

				descriptor.Type = type;
				descriptor.TypeName = type.ToString();

				var attr = typeof(TBlockChild).GetCustomAttribute<BlockTypeAttribute>();
				if (attr != null)
				{
					descriptor.Name = attr.Name;
					descriptor.Category = attr.Category;
					descriptor.Icon = attr.Icon;
					descriptor.Component = !string.IsNullOrEmpty(attr.Component) ? attr.Component : "missing-block";
				}
				else
				{
					throw new CustomAttributeFormatException("The BlockTypeDescriptor is mandatory.");
				}

				_blockDescriptors.Add(descriptor);
			}
		}

		public IEnumerable<string> GetCategories()
		{
			return _blockDescriptors
				.Select(b => b.Category)
				.Distinct()
				.OrderBy(c => c)
				.ToArray();
		}

		public IEnumerable<BlockTypeDescriptor> GetByCategory(string category)
		{
			var query = _blockDescriptors.Where(i => i.Category == category);

			return query.ToArray();
		}

		public BlockTypeDescriptor? GetByType(Type type)
		{
			return _blockDescriptors.SingleOrDefault(i => i.Type == type);
		}
	}
}
