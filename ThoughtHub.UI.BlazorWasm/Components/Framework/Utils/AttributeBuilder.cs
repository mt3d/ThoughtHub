namespace ThoughtHub.UI.BlazorWasm.Components.Framework
{
	public class CssStyleBuilder : AttributeBuilder
	{
		protected override char Separator { get => ';';  }
	}

	public class CssClassBuilder : AttributeBuilder
	{
		protected override char Separator { get => ' '; }
	}

	/// <summary>
	/// This abstract class provides the core mechanism for assembling attribute
	/// strings, such as CSS classes or inline styles. It's lazy string builder
	/// that concatenates attribute fragments (such as CSS classes or inline styles)
	/// using a separator. However, instead of storing strings directly, it stores functions that generate strings.
	/// 
	/// You insert functions that produce class/style strings. The machine stores
	/// those functions. It builds the final output only when you ask for it (when
	/// Value is accessed). When the state changes, you press Reset, meaning: rebuild next time.
	/// 
	/// Example usage:
	/// ClassBuilder.Register(() => "btn");
	/// ClassBuilder.Register(() => IsPrimary? "btn-primary" : null);
	/// ClassBuilder.Register(() => Disabled? "btn-disabled" : null);
	/// 
	/// Why they store functions instead of strings?
	/// ClassBuilder.Register(() => IsActive ? "active" : null);
	/// If IsActive changes, the builder can regenerate the correct output next render.
	/// No need to manually remove or add classes. No risk of stale values.
	/// </summary>
	public abstract class AttributeBuilder
	{
		protected abstract char Separator { get; }

		/// <summary>
		/// The builder uses a _dirty flag to determine whether the cached attribute
		/// string needs to be rebuilt. Whenever a parameter relevant to classes or
		/// styles changes, the builder resets this flag so the next access triggers a rebuild.
		/// </summary>
		private bool _needRebuild = true;

		/// <summary>
		/// The builder uses a lazy-building approach based on two lists of registrars.
		/// The first list contains simple functions returning a string. The second
		/// contains functions that accept an action to add more values along the way.
		/// Both allow a component to assemble attributes in a flexible and extensible way.
		/// </summary>
		private List<Func<string?>> _registrations = new();
		private List<Func<Action<string?>, string?>> _actionRegistrations = new();

		private string? _value;
		public string? Value
		{
			get
			{
				if (_needRebuild)
				{
					Build();
				}
				return _value;
			}
		}

		public AttributeBuilder Register(Func<string?> registration)
		{
			_registrations.Add(registration);
			return this;
		}

		/// <summary>
		/// If the underlying state changes, .Reset() marks the builder as needing
		/// rebuild, forcing .Value to rebuild everything next time.
		/// </summary>
		public void Reset()
		{
			_needRebuild = true;
		}

		/// <summary>
		/// The rebuilding process gathers all returned strings, filters out empty or
		/// null values, concatenates them using a separator, and stores the result.
		/// The separator itself is abstract and defined by derived builders.
		/// </summary>
		private void Build()
		{
			var values = new List<string?>();

			// Execute simple registrars like () => "btn", () => "active"
			var values1 = _registrations.Select(r => r());

			// Execute complex registrars that can add multiple values
			var values2 = _actionRegistrations
					.Select(ar => ar(values.Add))
					.ToArray();

			string? value = string.Join(Separator, values.Concat(values1).Concat(values2).Where(s => s is not null));

			_value = value is not null ? value : null;
			_needRebuild = false;
		}
	}
}
