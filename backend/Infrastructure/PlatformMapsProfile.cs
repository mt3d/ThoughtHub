using AutoMapper;
using backend.Data.Entities;
using backend.Data.Identity;
using backend.Logic.Users;

namespace backend.Infrastructure
{
	public class PlatformMapsProfile : AutoMapper.Profile
	{
		public PlatformMapsProfile()
		{
			CreateMap<User, UserDto>(MemberList.None);
		}
	}
}
