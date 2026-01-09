using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Services
{
	public interface IMediaUploadService
	{
		Task<Image> SaveImageAsync(IFormFile file);
	}
}
