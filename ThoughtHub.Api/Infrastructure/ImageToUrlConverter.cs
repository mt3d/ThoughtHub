using AutoMapper;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Services;

namespace ThoughtHub.Infrastructure
{
	public class ImageToUrlConverter : ITypeConverter<Image, string?>
	{
		private readonly IMediaService _mediaService;

		public ImageToUrlConverter(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public string? Convert(Image source, string? destination, ResolutionContext context)
		{
			if (source == null)
				return null;

			return _mediaService.GetOrCreateVersionAsync(source, source.Width, source.Height).Result;
		}
	}
}
