using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Api.Models.Editor;
using ThoughtHub.UI.BlazorWasm.Components.EditorBlocks;

namespace ThoughtHub.UI.BlazorWasm.Components.Editor
{
	public partial class Editor
	{
		[Inject]
		public IHttpClientFactory? ClientFactory { get; set; }
		private HttpClient? httpClient;
		private JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

		public ArticleEditModel ArticleModel { get; set; }

		[CascadingParameter]
		public IModalService Modal { get; set; } = default!;

		protected override Task OnInitializedAsync()
		{
			httpClient = ClientFactory?.CreateClient("Auth");

			ArticleModel = new ArticleEditModel()
			{

			};

			return base.OnInitializedAsync();
		}

		private static readonly Dictionary<string, Type> BlockComponents = new()
		{
			["text-block"] = typeof(EditorTextBlock)
		};

		private Type? GetComponentType(string name)
		{
			// TODO: Should we add a default block in case of errors?
			return BlockComponents.TryGetValue(name, out var t) ? t : null;
		}

		private async Task OnBlockPicked((string type, int index) result)
		{
			await AddBlockAsync(result.type, result.index);
		}

		public async Task AddBlockAsync(string type, int position)
		{
			try
			{
				var block = await httpClient.GetFromJsonAsync<BlockEditModel>($"/content/block/{type}", options);

				if (block != null)
				{
					Console.WriteLine(block.Block is ThoughtHub.Api.Models.Blocks.TextBlockModel); // False
					ArticleModel.Blocks.Insert(position, block);
				}
			}
			catch (Exception ex)
			{
				// TODO: Find a better way to report the error.
				Console.WriteLine($"error: {ex.Message}");
			}
		}

		public void MoveBlock(int from, int to)
		{
			if (from < 0 || from >= ArticleModel.Blocks.Count)
			{
				return;
			}

			if (to < 0 || to > ArticleModel.Blocks.Count)
			{
				return;
			}

			var block = ArticleModel.Blocks[from];
			ArticleModel.Blocks.RemoveAt(from);
			ArticleModel.Blocks.Insert(to, block);
		}

		public void RemoveBlock(BlockEditModel block)
		{
			if (block == null)
			{
				return;
			}

			ArticleModel.Blocks.Remove(block);
		}

		public async Task OpenBlockPicker(int position)
		{
			try
			{
				var list = await httpClient.GetFromJsonAsync<BlockListModel>("/content/blocktypes");

				Console.WriteLine(list.Categories.ToString());

				if (list is not null)
				{
					if (list.TypeCount == 1)
					{

					}
					else
					{
						var modal = Modal.Show<BlockPickerModal>("Select block", new ModalParameters()
							.Add(nameof(BlockPickerModal.Index), position)
							.Add(nameof(BlockPickerModal.Categories), list.Categories)
							.Add(nameof(BlockPickerModal.OnSelected), EventCallback.Factory.Create<(string, int)>(this, OnBlockPicked)));
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"error: {ex.Message}");
			}
		}

		private string GetBlockClasses(BlockEditModel block)
		{
			var classes = $"block + {block.Component}";

			if (block.IsCollapsed)
			{
				classes += " collapsed";
			}

			if (block.EditorWidth == "full")
			{
				classes += " block-full";
			}

			return classes;
		}
	}
}
