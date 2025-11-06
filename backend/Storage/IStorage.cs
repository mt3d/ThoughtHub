using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Storage
{
	public interface IStorage
	{
		Task<string> PutAsync(Image image, string filename, Stream stream);
	}
}
