using AutoMapper;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Contracts.User;
using yuapi.Domain.Entities;

namespace yuapi.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<InterfaceInfoAddRequest, InterfaceInfo>();
            CreateMap<UserRegisterRequest, User>();
            CreateMap<User, UserSafetyResponse>();
            CreateMap<SearchUserRequest, User>();
        }
    }
}
