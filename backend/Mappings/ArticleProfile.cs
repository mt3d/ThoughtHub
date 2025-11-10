using ThoughtHub.Data.Entities;
using ThoughtHub.Api.Models;
using ThoughtHub.Data.Entities.Publications;

namespace ThoughtHub.Mappings
{
	public class ArticleProfile : AutoMapper.Profile
	{
		public ArticleProfile()
		{
			CreateMap<Article, ArticleModel>()
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src =>
					src.Publication == null
						? $"@{src.AuthorProfile.User.UserName}/{src.Slug}"
						: $"{src.Publication.Slug}/{src.Slug}")
				)
				.ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorProfile))
				.ForMember(dest => dest.Publication, opt => opt.MapFrom(src => src.Publication));

			CreateMap<Data.Entities.Profile, AuthorModel>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.User.UserName))
				.ForMember(dest => dest.ProfilePic, opt =>
				{
					opt.MapFrom(src => src.ProfilePicture);
				}); // TODO: Fix

			CreateMap<Publication, PublicationModel>()
				.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Slug));

			CreateMap<Tag, TagModel>();
		}
	}
}
