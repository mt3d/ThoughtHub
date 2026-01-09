using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using ThoughtHub.UI.BlazorWasm.Infrastructure.Models;

namespace ThoughtHub.UI.BlazorWasm.Components.Modals
{
	public partial class SigninEmailControls
	{
		[Parameter]
		public EventCallback<HomeModalMode> OnSwitchMode { get; set; }

		[Parameter]
		public EventCallback Close { get; set; }

		private FormResult formResult = new();

		[SupplyParameterFromForm]
		private InputModel Input { get; set; } = new();

		[SupplyParameterFromQuery]
		private string? ReturnUrl { get; set; }

		public async Task LoginUserAsync()
		{
			formResult = await Account.SigninAsync(Input.Email, Input.Password);

			if (formResult.Succeeded)
			{
				if (!string.IsNullOrEmpty(ReturnUrl))
				{
					Navigation.NavigateTo(ReturnUrl);
				}

				Close.InvokeAsync();
			}
		}

		// TOOD: Move out of here.
		private sealed class InputModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; } = string.Empty;

			[Required]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; } = string.Empty;
		}
	}
}
