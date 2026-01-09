using ThoughtHub.UI.BlazorWasm.Infrastructure.Models;

namespace ThoughtHub.UI.BlazorWasm.Infrastructure
{
	public interface IAccountManagement
	{
		public Task<FormResult> SigninAsync(string email, string password);
		public Task SignoutAsync();
		public Task<FormResult> RegisterAsync(string email, string password);
		public Task<bool> CheckAuthenticatedState();
	}
}
