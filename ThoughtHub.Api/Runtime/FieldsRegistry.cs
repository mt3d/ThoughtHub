using System.Reflection;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Runtime
{
	public class FieldsRegistry
	{
		/// <summary>
		/// We store a BlockTypeDescriptor not BlockModel. Each instance of BlockModel
		/// represents a single block. This is however is a registry of the available blocks,
		/// and that's why we need a dedicated TypeDescriptor.
		/// </summary>
		private readonly List<FieldTypeDescriptor> _fieldDescriptors = new List<FieldTypeDescriptor>();

		public void Register<TFieldChild>() where TFieldChild : IFieldModel
		{
			var type = typeof(TFieldChild);
			var item = Activator.CreateInstance<TFieldChild>();

			if (_fieldDescriptors.Where(f => f.Type == type).Count() == 0)
			{
				FieldTypeDescriptor descriptor = new FieldTypeDescriptor();

				descriptor.Type = type;
				descriptor.TypeName = type.ToString();

				var attr = typeof(TFieldChild).GetCustomAttribute<FieldTypeAttribute>();
				if (attr != null)
				{
					descriptor.Name = attr.Name;
					descriptor.EditorComponent = !string.IsNullOrEmpty(attr.EditorComponent) ? attr.EditorComponent : "missing-field";
				}
				else
				{
					throw new CustomAttributeFormatException("The FieldTypeDescriptor is mandatory.");
				}

				_fieldDescriptors.Add(descriptor);
			}
		}

		public FieldTypeDescriptor? GetByType(Type type)
		{
			return _fieldDescriptors.SingleOrDefault(i => i.Type == type);
		}

		public FieldTypeDescriptor? GetByTypeName(string typeName)
		{
			if (typeName != null)
			{
				// TODO: Handle generic types.
			}

			return _fieldDescriptors.SingleOrDefault(b => b.TypeName == typeName);
		}
	}
}
