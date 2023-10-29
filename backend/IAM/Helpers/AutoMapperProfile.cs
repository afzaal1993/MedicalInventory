using AutoMapper;
using IAM.DTOs;
using IAM.Models;

namespace IAM.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UpdateUserDto, AppUser>();
        }
    }
}
