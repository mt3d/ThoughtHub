using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Framework
{
	public abstract class HubComponentBase : ComponentBase
	{
		private ComponentDirection _direction;

		/// <summary>
		/// Custom css class for the root element.
		/// </summary>
		[Parameter]
		public string? Class { get; set; }

		[Parameter]
		public bool IsEnabled { get; set; } = true;

		private ComponentDirection? _dir;
		[Parameter]
		public ComponentDirection? Direction
		{
			get => _dir; // TODO: Inherit cascading direction
			set => _dir = value;
		}

		protected CssStyleBuilder StyleBuilder { get; private set; } = new CssStyleBuilder();

		protected CssClassBuilder ClassBuilder { get; private set; } = new CssClassBuilder();

		protected abstract string RootElementClass { get; }

		protected virtual void RegisterCssClasses() { }

		/// <summary>
		/// This methos is invoked when the component is first initialized.
		/// </summary>
		protected override void OnInitialized()
		{
			// TODO: Register styles.

			ClassBuilder
				.Register(() => RootElementClass)
				.Register(() => IsEnabled ? string.Empty : "hub-disabled")
				.Register(() => Direction == ComponentDirection.Rtl ? "hub-rtl" : string.Empty);

			RegisterCssClasses();

			ClassBuilder.Register(() => Class);

			base.OnInitialized();
		}
	}
}
