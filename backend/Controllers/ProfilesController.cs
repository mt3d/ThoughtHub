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
		private readonly IMediaUploadService _mediaService;

		public ProfilesController(
			PlatformContext context,
			IMediaUploadService mediaService)
		{
			_context = context;
			_mediaService = mediaService;
		}

		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> SetProfileImage(IFormFile file)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var profile = (await _context.Profiles.FirstAsync(p => p.UserId == userId));

			var image = await _mediaService.SaveImageAsync(file);

			profile.ProfilePicture = image;

			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
