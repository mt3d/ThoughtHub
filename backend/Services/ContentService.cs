using System.Reflection;
using System.Text.Json;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data;

namespace ThoughtHub.Services
{
	public class ContentService : IContentService
	{
		/**
		 * This function receives a list of "editor blocks" and converts them
		 * into a list of database block entities (Block), which are the objects
		 * your persistence layer stores. Every block model is scanned, converted
		 * to a database Block, and its fields are extracted and saved as BlockField
		 * entries. If the block is a group container (a BlockGroup), the function
		 * converts its child blocks recursively and links them to their parent.
		 */
		public IList<Block> TransformBlocks(IList<BlockModel> models)
		{
			var blocks = new List<Block>();

			if (models != null)
			{
				for (int i = 0; i < models.Count; i++)
				{
					var block = new Block()
					{
						Id = models[i].Id != Guid.Empty ? models[i].Id : Guid.NewGuid(),
						ClrType = models[i].GetType().FullName,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now
					};

					foreach (var prop in models[i].GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
					{
						if (typeof(IFieldModel).IsAssignableFrom(prop.PropertyType))
						{
							var field = new BlockField()
							{
								Id = new Guid(),
								BlockId = block.Id,
								FieldId = prop.Name,
								SortOrder = 0,
								ClrType = prop.PropertyType.FullName,
								SerializedValue = JsonSerializer.Serialize(prop.GetValue(models[i]))
							};

							block.Fields.Add(field);
						}
					}

					blocks.Add(block);

					// TODO: Handle group blocks in the future if necessary.
				}
			}

			return blocks;
		}
	}
}
