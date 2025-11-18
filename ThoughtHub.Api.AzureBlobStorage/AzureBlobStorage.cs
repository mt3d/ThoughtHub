using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Storage;

namespace ThoughtHub.Api.AzureBlobStorage
{
	public class AzureBlobStorage : IStorage
	{
		public Task<bool> DeleteAsync(Image image)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteAsync(ImageVersion version)
		{
			throw new NotImplementedException();
		}

		public Task<bool> GetAsync(Image image, Stream stream)
		{
			throw new NotImplementedException();
		}

		public string? GetPublicUrl(Image image)
		{
			throw new NotImplementedException();
		}

		public Task<string> PutAsync(Image image, Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
