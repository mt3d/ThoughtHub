using ThoughtHub.Api.Core.Entities.Article;
using ThoughtHub.Api.Models;
using ThoughtHub.Api.Models.Content;
using ThoughtHub.Data.Entities.Publications;
using ThoughtHub.Data.Entities;
using ThoughtHub.Data.Entities.Media;
using ThoughtHub.Api.Models.RecommendedPublishers;

namespace ThoughtHub.Infrastructure
{
	public class PlatformMapsProfile : AutoMapper.Profile
	{
		public PlatformMapsProfile()
		{
			CreateMap<Image, string?>().ConvertUsing<ImageToUrlConverter>();

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

			CreateMap<Article, ArticleDigestModel>()
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


			CreateMap<Publication, PublicationPublisherModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicationId.ToString()))
				.ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
				.ForMember(dest => dest.PublicationImageUrl, opt => opt.MapFrom(src => src.PublicationImage));

			CreateMap<Data.Entities.Profile, UserPublisherModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
				.ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicture));

			CreateMap<Tag, TagModel>();
		}
	}
}
