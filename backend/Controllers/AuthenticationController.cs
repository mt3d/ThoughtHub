using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThoughtHub.Data.Identity;

namespace ThoughtHub.Controllers
{
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly SignInManager<User> _signInManager;

		public AuthenticationController(SignInManager<User> signInManager)
		{
			_signInManager = signInManager;
		}

		[Authorize]
		[HttpPost("/logout")]
		public async Task<IActionResult> Logout([FromBody] object empty)
		{
			if (empty is not null)
			{
				await _signInManager.SignOutAsync();

				return Ok();
			}

			return Unauthorized();
		}
	}
}
