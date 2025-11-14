using System.Text;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Storage;

namespace ThoughtHub.Api.LocalStorage
{
	// TODO: Default filename is image.FileName
	public class LocalStorage : IStorage
	{
		private readonly string _basePath = "wwwroot/uploads/";
		private readonly string _baseUrl = "~/uploads/";
		private readonly LocalStorageNaming _naming;

		public LocalStorage(
			string? baseUrl = null,
			LocalStorageNaming naming = LocalStorageNaming.UniqueFileNames)
		{
			if (!string.IsNullOrEmpty(baseUrl))
			{
				_baseUrl = baseUrl;
			}

			_naming = naming;
		}

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

		public async Task<string> PutAsync(Image image, Stream stream)
		{
			var path = BuildImageName(image);

			// TODO: Check if the subfolder exists, in case subfolders are used.

			using (var file = File.OpenWrite(_basePath + path))
			{
				await stream.CopyToAsync(file).ConfigureAwait(false);
			}

			return _baseUrl + path;
		}

		public string? GetPublicUrl(Image image)
		{
			if (image is null)
			{
				return null;
			}

			var publicUrl = _baseUrl + BuildImageName(image);

			// TODO: Handle version hash

			return publicUrl;
		}

		// TODO: Move to IStorage. IStorage should get Image or ImageVersion, and it should then
		// build the name by itself. We shouldn't be responsible for providing it with the name.
		// Common for all storage providers
		// Could be considered an implementation detail of the storage provider.
		private string? BuildImageName(Image image, int? width = null, int? height = null)
		{
			if (image is null)
			{
				return null;
			}

			var filename = new FileInfo(image.Filename);
			var sb = new StringBuilder();

			// To ensure unique names.
			sb.Append(image.Id);

			if (_naming == LocalStorageNaming.UniqueFileNames)
			{
				sb.Append("_");
			}
			else
			{
				sb.Append("/");
			}

			// TODO: Handle encoding
			// TODO: Add optional parameter rfor encoding

			if (width.HasValue)
			{
				// example.jpg => example_
				sb.Append(filename.Name.Replace(filename.Extension, "_"));
				sb.Append(width);

				if (height.HasValue)
				{
					sb.Append("x");
					sb.Append(height);
				}
			}
			else
			{
				sb.Append(filename.Name.Replace(filename.Extension, ""));
			}

			// TODO: Handle optional extension
			sb.Append(filename.Extension);

			// TODO: Handle the version hash.
			var versionHash = Math.Abs(image.UpdatedAt.Ticks.GetHashCode()).ToString();

			return sb.ToString();
		}
	}
}
