using frontend_blazorwasm.Infrastructure.Models;

namespace frontend_blazorwasm.Infrastructure
{
	public interface IAccountManagement
	{
		public Task<FormResult> SigninAsync(string email, string password);
		public Task SignoutAsync();
		public Task<FormResult> RegisterAsync(string email, string password);
		public Task<bool> CheckAuthenticatedState();
	}
}
