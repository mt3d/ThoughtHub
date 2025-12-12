using AutoMapper;
using System.Reflection;
using System.Text.Json;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Runtime;

namespace ThoughtHub.Services
{
	public class ContentService : IContentService
	{
		private readonly BlocksRegistry _blocksRegistry;
		private readonly FieldsRegistry _fieldsRegistry;
		private readonly IMapper _mapper;

		public ContentService(
			BlocksRegistry blocksRegistry,
			FieldsRegistry fieldsRegistry,
			IMapper mapper)
		{
			_blocksRegistry = blocksRegistry;
			_fieldsRegistry = fieldsRegistry;
			_mapper = mapper;
		}

		public ArticleM TransformArticleEntityIntoModel(Article article)
		{
			var model = new ArticleM();

			// TODO: Initialized model?

			// Map basic properties
			_mapper.Map(article, model);

			// TODO: Transform comments

			// Transform blocks

			if (article.Blocks.Count > 0)
			{
				foreach (var articleBlock in article.Blocks.OrderBy(b => b.SortOrder))
				{
					// Handle parents
				}

				model.BlockModels = TransformBlockEntitiesIntoModels(article.Blocks.OrderBy(b => b.SortOrder).Select(b => b.Block));
			}

			return model;
		}

		public Article Transform(ArticleM model)
		{
			var article = new Article();

			if (model.Id != Guid.Empty)
			{
				article.Id = model.Id;
			}
			else
			{
				article.Id = model.Id = Guid.NewGuid();
			}
			article.CreatedAt = DateTime.Now;

			// TODO: Map basic properties

			throw new NotImplementedException();
		}

		/**
		 * This function receives a list of "block models" and converts them
		 * into a list of database block entities (Block), which are the objects
		 * the persistence layer stores. Every block model is scanned, converted
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

		public IList<BlockModel> TransformBlockEntitiesIntoModels(IEnumerable<Block> blocks)
		{
			var models = new List<BlockModel>();

			foreach (var block in blocks)
			{
				var blockTypeDescriptor = _blocksRegistry.GetByTypeName(block.ClrType);

				if (blockTypeDescriptor is not null)
				{
					var model = (BlockModel?)Activator.CreateInstance(blockTypeDescriptor.Type);

					if (model is not null)
					{
						model.Id = block.Id;
						model.Type = block.ClrType;

						// For each property of the type IFieldModel in the BlockModel.
						foreach (var prop in model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase))
						{
							if (typeof(IFieldModel).IsAssignableFrom(prop.GetType()))
							{
								// Pair which one of the current block fields in the database is the value of this property.
								var field = block.Fields.FirstOrDefault(f => f.FieldId == prop.Name);

								if (field != null)
								{ 
									var fieldTypeDescriptor = _fieldsRegistry.GetByTypeName(field.ClrType);

									if (fieldTypeDescriptor is not null)
									{
										var value = (IFieldModel?)JsonSerializer.Deserialize(field.SerializedValue, fieldTypeDescriptor.Type);

										prop.SetValue(model, value);
									}
								}
								else
								{
									// Empty TextField, ImageField, etc?
									prop.SetValue(model, Activator.CreateInstance(prop.PropertyType));
								}
							}
						}

						// TODO: Handle parent

						models.Add(model);
					}
				}
			}

			return models;
		}
	}
}
