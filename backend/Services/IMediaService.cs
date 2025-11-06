using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Services
{
	public interface IMediaService
	{
		Task<Image> SaveAsync(string filename, Stream content);
	}
}
