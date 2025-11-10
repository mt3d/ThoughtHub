using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Storage
{
	public interface IStorage
	{
		/// <summary>
		/// Writes the content of the specified image to the given stream.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="stream"></param>
		/// <returns></returns>
		Task<bool> GetAsync(Image image, Stream stream);

		// Task<string> PutAsync(Image image, string filename, Stream stream);
		Task<string> PutAsync(Image image, Stream stream);

		Task<bool> DeleteAsync(Image image);
		Task<bool> DeleteAsync(ImageVersion version);

		//string GetPublicUrl(Image image, string filename);
		string? GetPublicUrl(Image image);
	}
}
