using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.UI.BlazorWasm.Components.Editor
{
	public partial class Editor
	{
		[Inject]
		public IHttpClientFactory? ClientFactory { get; set; }
		private HttpClient? httpClient;
		private JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

		public ArticleEditModel ArticleModel { get; set; }

		protected override Task OnInitializedAsync()
		{
			httpClient = ClientFactory?.CreateClient("Auth");

			ArticleModel = new ArticleEditModel();

			return base.OnInitializedAsync();
		}

		public async Task AddBlockAsync(string type, int position)
		{
			try
			{
				var block = await httpClient.GetFromJsonAsync<BlockModel>($"/content/block/{type}", options);

				if (block != null)
				{
					ArticleModel.Blocks.Insert(position, block);
				}
			}
			catch (Exception ex)
			{
				// TODO: Find a better way to report the error.
				Console.WriteLine($"error: {ex.Message}");
			}
		}
	}
}
