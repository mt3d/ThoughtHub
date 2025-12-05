using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Spacer
{
	public partial class HubSpacer : HubComponentBase
	{
		[Parameter]
		public int? Width { get; set; }

		protected override string RootElementClass => "hub-spacer";

		protected override void RegisterCssClasses()
		{
			StyleBuilder.Register(() => Width.HasValue ? $"margin-inline-start:{Width}px" : "flex-grow:1");
		}
	}
}
