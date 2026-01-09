using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Services
{
	public interface IMediaService
	{
		Task<Image> GetByIdAsync(Guid id);

		Task<Image> AddAsync(string filename, Stream content);

		Task<string?> GetOrCreateVersionAsync(Image image, int? width, int? height);
	}
}
