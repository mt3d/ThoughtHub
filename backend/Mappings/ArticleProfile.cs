using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Data.Entities.Publications;
using ThoughtHub.Services;

namespace ThoughtHub.Mappings
{
	public class ImageToUrlConverter : ITypeConverter<Image, string>
	{
		private readonly IMediaService _mediaService;

		public ImageToUrlConverter(IMediaService mediaService)
		{
			_mediaService = mediaService;
		}

		public string? Convert(Image source, string destination, ResolutionContext context)
		{
			if (source == null)
				return null;

			return _mediaService.GetOrCreateVersionAsync(source, source.Width, source.Height).Result;
		}
	}

	public class ArticleProfile : AutoMapper.Profile
	{
		public ArticleProfile()
		{
			CreateMap<Image, string>().ConvertUsing<ImageToUrlConverter>();

			// Map Article model to Article entity
			CreateMap<ArticleModel, Article>();

			CreateMap<Article, ArticleModel>()
				// TODO: Move to permalink.
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src =>
					src.Publication == null
						? $"@{src.AuthorProfile.User.UserName}/{src.Slug}"
						: $"{src.Publication.Slug}/{src.Slug}")
				)
				.ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorProfile))
				.ForMember(dest => dest.Publication, opt => opt.MapFrom(src => src.Publication));

			CreateMap<Article, ArticleCardModel>()
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src =>
					src.Publication == null
						? $"@{src.AuthorProfile.User.UserName}/{src.Slug}"
						: $"{src.Publication.Slug}/{src.Slug}")
				)
				.ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorProfile))
				.ForMember(dest => dest.Publication, opt => opt.MapFrom(src => src.Publication))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ArticleImage));

			CreateMap<Data.Entities.Profile, AuthorModel>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.User.UserName))
				.ForMember(dest => dest.ProfilePic, opt => opt.MapFrom(src => src.ProfilePicture));

			CreateMap<Publication, PublicationModel>()
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Slug))
				.ForMember(dest => dest.PublicationImageUrl, opt => opt.MapFrom(src => src.PublicationImage));

			CreateMap<Tag, TagModel>();
		}
	}
}
