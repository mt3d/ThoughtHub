using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities;

namespace ThoughtHub.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly PlatformContext _context;

		private Profile _cachedProfile;
		private bool _profileLoaded;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor, PlatformContext context)
		{
			_httpContextAccessor = httpContextAccessor;
			_context = context;
		}

		public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

		public string UserId
		{
			get
			{
				// TODO: Add a detailed explanation.
				var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

				if (string.IsNullOrEmpty(userId))
				{
					throw new UnauthorizedAccessException("User is not authenticated");
				}

				return userId;
			}
		}

		public async Task<Profile> GetProfileAsync()
		{
			if (_profileLoaded)
			{
				return _cachedProfile;
			}

			if (!IsAuthenticated)
			{
				throw new UnauthorizedAccessException("User is not authenticated");
			}

			_cachedProfile = await _context.Profiles
				.Include(p => p.User)
				.FirstOrDefaultAsync(p => p.UserId == UserId)
				?? throw new InvalidOperationException("No profile found for the current user");

			_profileLoaded = true;
			return _cachedProfile;
		}
	}
}
