using Microsoft.AspNetCore.Mvc;

namespace ThoughtHub.Services
{
	public interface IMediaService
	{
		string UploadImageAsync(int profileId, IFormFile file);
	}
}
