using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.UI.BlazorWasm.Components.Editor
{
	public partial class Editor
	{
		public ArticleEditModel ArticleModel { get; set; }

		protected override Task OnInitializedAsync()
		{
			ArticleModel = new ArticleEditModel();

			return base.OnInitializedAsync();
		}
	}
}
