using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
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
				Slug = string.Empty,
				Status = new StatusMessage { Body = string.Empty, Type = string.Empty },
				OgTitle = string.Empty,
				MetaTitle = string.Empty,
				Published = string.Empty,
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
				block.Title = string.Empty;
				block.Description = string.Empty;
				block.EditorWidth = string.Empty;
				block.Placeholder = string.Empty;

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
			var classes = $"block {block.Component}";

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

		private bool _saving = false;

		// Publish
		public async Task Save()
		{
			_saving = true;
			await SaveInternal();
		}

		private async Task SaveInternal()
		{
			try
			{
				var response = await httpClient.PostAsJsonAsync<ArticleEditModel>($"/articles/save", ArticleModel, options);
				response.EnsureSuccessStatusCode();

				var result = await response.Content.ReadFromJsonAsync<ArticleEditModel>();
				ArticleModel.Id = result.Id;
				ArticleModel.Slug = result.Slug;
				// TODO: Update published.

				_saving = false;

			}
			catch (Exception ex)
			{
				// TODO: Find a better way to report the error.
				Console.WriteLine($"error: {ex.Message}");
			}
		}

		private void OnUpdateBlockTitle((string uid, string title) args)
		{
			foreach (var block in ArticleModel.Blocks)
			{
				if (block.Uid == args.uid)
				{
					block.Title = args.title;
					return;
				}
			}	
		}
	}
}
