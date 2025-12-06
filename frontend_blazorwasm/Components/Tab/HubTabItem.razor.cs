using Microsoft.AspNetCore.Components;

namespace ThoughtHub.UI.BlazorWasm.Components.Tab
{
	public partial class HubTabItem : HubComponentBase
	{
		[CascadingParameter]
		private HubTab? _parent { get; set; }

		#region public_params

		/// <summary>
		/// This parameter can be used to display a custom complex header.
		/// </summary>
		[Parameter]
		public RenderFragment? Header { get; set; }

		/// <summary>
		/// This parameter can be used if you just want to display text in the header.
		/// </summary>
		[Parameter]
		public string? HeaderText { get; set; }

		/// <summary>
		/// This is used to supply the content of the tab when using custom headers.
		/// </summary>
		[Parameter]
		public RenderFragment? Body { get; set; }

		[Parameter]
		public RenderFragment? ChildContent { get; set; }

		[Parameter]
		public string? Key { get; set; }

		private bool _isSelected;

		// Two way bound.
		// Should reset class builder.
		[Parameter]
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (!EqualityComparer<bool>.Default.Equals(_isSelected, value))
				{
					_isSelected = value;
					ClassBuilder.Reset();
				}
			}
		}

		[Parameter]
		public EventCallback<bool?> SelectedKeyChanged { get; set; }

		#endregion

		protected override string RootElementClass => "hub-tab-item";

		protected override async Task OnInitializedAsync()
		{
			_parent?.RegisterItem(this);

			await base.OnInitializedAsync();
		}

		protected override void RegisterCssClasses()
		{
			ClassBuilder.Register(() => IsSelected ? $"hub-tab-item-selected" : string.Empty);
		}

		internal void SetIsSelected(bool value)
		{
			IsSelected = value;
			SelectedKeyChanged.InvokeAsync(value);

			StateHasChanged();
		}

		private async Task HandleOnClick()
		{
			// TODO: Check if the tab is enabled.

			_parent?.SelectItem(this);
		}
	}
}
