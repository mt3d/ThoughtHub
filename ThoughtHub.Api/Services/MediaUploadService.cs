using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Storage;

namespace ThoughtHub.Services
{
	public class MediaUploadService : IMediaUploadService
	{
		private readonly IStorage _storage;
		private readonly IMediaService _mediaService;

		public MediaUploadService(
			IStorage storage,
			IMediaService mediaService)
		{
			_storage = storage;
			_mediaService = mediaService;
		}

		public async Task<Image?> SaveImageAsync(IFormFile file)
		{
			if (file.Length > 0 && !string.IsNullOrWhiteSpace(file.ContentType))
			{
				using var stream = file.OpenReadStream();

				return await _mediaService.AddAsync(file.FileName, stream);
			}

			return null;
		}
	}
}
