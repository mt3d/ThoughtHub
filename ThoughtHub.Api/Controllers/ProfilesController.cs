using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Data;
using ThoughtHub.Services;

namespace ThoughtHub.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProfilesController : ControllerBase
	{
		private readonly PlatformContext _context;
		private readonly IMediaUploadService _mediaService;
		private readonly ICurrentUserService _currentUserService;

		public ProfilesController(
			PlatformContext context,
			IMediaUploadService mediaService,
			ICurrentUserService currentUserService)
		{
			_context = context;
			_mediaService = mediaService;
			_currentUserService = currentUserService;
		}

		[HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> SetProfileImage(IFormFile file)
		{
			var profile = await _currentUserService.GetProfileAsync();

			var image = await _mediaService.SaveImageAsync(file);

			profile.ProfilePicture = image;

			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
