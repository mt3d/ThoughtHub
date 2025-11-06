using ThoughtHub.Data;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Storage;

namespace ThoughtHub.Services
{
	public class MediaService
	{
		private readonly IStorage _storage;
		private readonly IMediaRepository _mediaRepository;
		// private readonly ICacheManger cacheManger;

		public MediaService(IStorage storage, IMediaRepository mediaRepository)
		{
			_storage = storage;
			_mediaRepository = mediaRepository;
		}

		// TODO: return saved Media
		public async Task<Image> SaveAsync(string filename, Stream contentStream)
		{
			// TODO: Check content type.

			Image model = null;

			model = new Image
			{
				// TODO: Automate
				CreatedAt = DateTime.Now
			};
			model.Filename = filename.Replace(" ", "_");
			// TODO: Handle folder
			// TODO: Handle content type
			model.UpdatedAt = DateTime.Now;

			// TODO: Preprocesse the image
			// TODO: Get image dimensions

			model.Size = contentStream.Length;

			await _storage.PutAsync(model, model.Filename, contentStream).ConfigureAwait(false);

			await _mediaRepository.AddOrUpdate(model).ConfigureAwait(false);

			await RemoveFromCache(model).ConfigureAwait(false);
			// TODO: Remove the media structure from cache

			return model;
		}

		private async Task RemoveFromCache(Image model)
		{
			// TODO: Remove image from cache manager
		}
	}
}
