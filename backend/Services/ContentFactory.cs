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

						// TODO: Create a loop that populates all field properties.

						return block;
					}
				}
			}

			return null;
		}
	}
}
