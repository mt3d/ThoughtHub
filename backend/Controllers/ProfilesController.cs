using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Data;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	public class ProfilesController : Controller
	{
		private readonly PlatformContext _context;
		private readonly IMediaService _mediaService;

		public async Task<IActionResult> UploadProfileImage(IFormFile file)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profileId = (await _context.Profiles.FirstAsync(p => p.UserId == userId)).ProfileId;

			var url = _mediaService.UploadImageAsync(profileId, file);

			return Ok(new { imageUrl = url });
		}
	}
}
