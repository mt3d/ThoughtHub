using System.Text;
using ThoughtHub.Data;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Storage;

namespace ThoughtHub.Services
{
	public class MediaService : IMediaService
	{
		private readonly IStorage _storage;
		private readonly IMediaRepository _mediaRepository;
		// private readonly ICacheManger cacheManger;

		public MediaService(IStorage storage, IMediaRepository mediaRepository)
		{
			_storage = storage;
			_mediaRepository = mediaRepository;
		}

		public async Task<Image> GetByIdAsync(Guid id)
		{
			// TODO: Use cache.
			return await _mediaRepository.GetById(id).ConfigureAwait(false);
		}

		// TODO: return saved Media
		// TODO: What about CreateOrUpdate or Save
		public async Task<Image> AddAsync(string filename, Stream contentStream)
		{
			// TODO: Check content type.

			Image model = new Image();

			// make sure to init the Id here, so the storage can use when naming the file.
			model.Id = Guid.NewGuid();

			model.CreatedAt = DateTime.Now;
			model.Filename = filename.Replace(" ", "_");
			// TODO: Handle folder
			// TODO: Handle content type
			model.UpdatedAt = DateTime.Now;
			model.ImageFolderId = null;
			// TODO: Preprocesse the image
			// TODO: Get image dimensions

			model.Size = contentStream.Length;

			await _storage.PutAsync(model, contentStream).ConfigureAwait(false);

			await _mediaRepository.AddOrUpdate(model).ConfigureAwait(false);

			await RemoveFromCache(model).ConfigureAwait(false);
			// TODO: Remove the media structure from cache

			return model;
		}

		private async Task RemoveFromCache(Image model)
		{
			// TODO: Remove image from cache manager
		}

		public async Task<string?> GetOrCreateVersionAsync(Image image, int? width, int? height)
		{
			// TODO: Check if the current width equals the original width.
			if (width.HasValue && image.Width == width)
			{
				return GetImageUrl(image);
			}

			// TODO: Check if width has value
			var query = image.Versions.Where(v => v.Width == width);
			var version = query.FirstOrDefault();

			if (version != null)
			{
				return GetImageUrl(version);
			}

			// If version not found, create it

			// Get the base version
			using (var inputStream = new MemoryStream())
			{
				if (!await _storage.GetAsync(image, inputStream))
				{
					return null;
				}

				inputStream.Position = 0;

				using (var outputStream = new MemoryStream())
				{
					bool upload = false;

					// TODO: Try to process the image
					// TODO: Processing might change the extension

					if (upload)
					{
						await _storage.PutAsync(image, outputStream).ConfigureAwait(false);
					}
				}
			}

			// TODO: Process the image.
			return GetImageUrl(image);
		}

		private string GetImageUrl(ImageVersion image)
		{
			return GetImageUrl(image.BaseImage, image.Width, image.Height);
		}

		// TODO: Fix. The dimesnsions are never used.
		public string GetImageUrl(Image image, int? width = null, int? height = null)
		{
			//var name = BuildResourceFileName(image, height, width);

			// TODO: Handle CDN

			return _storage.GetPublicUrl(image);
		}
	}
}
