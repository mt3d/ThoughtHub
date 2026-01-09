using System.Reflection;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Runtime;

namespace ThoughtHub.Services
{
	public class ContentFactory : IContentFactory
	{
		private readonly BlocksRegistry _registry;
		private readonly IServiceProvider _services;

		public ContentFactory(BlocksRegistry registry, IServiceProvider services)
		{
			_registry = registry;
			_services = services;
		}

		public async Task<object?> CreateBlockAsync(string typeName)
		{
			var blockTypeDescriptor = _registry.GetByTypeName(typeName);

			if (blockTypeDescriptor is not null)
			{
				using (var scope = _services.CreateScope())
				{
					var block = (BlockModel?)Activator.CreateInstance(blockTypeDescriptor.Type);
					
					if (block is not null)
					{
						block.Type = typeName;

						// Find each property of the block that is derived from IFieldModel
						foreach (var prop in blockTypeDescriptor.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
						{
							if (typeof(IFieldModel).IsAssignableFrom(prop.PropertyType))
							{
								var field = Activator.CreateInstance(prop.PropertyType);

								prop.SetValue(block, field);
							}
						}

						return block;
					}
				}
			}

			return null;
		}
	}
}
