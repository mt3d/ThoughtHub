namespace ThoughtHub.UI.BlazorWasm.Infrastructure.Models
{
	/// <summary>
	/// Represents the result of a login or registration form.
	/// </summary>
	public class FormResult
	{
		/// <summary>
		/// Gets or sets a value indicating whether the form submission was successful.
		/// </summary>
		public bool Succeeded { get; set; }

		/// <summary>
		/// When the action fails, the errors are stored in this array.
		/// </summary>
		public string[] ErrorList { get; set; } = [];
	}
}
