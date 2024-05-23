using AutoMapper;
using yuapi.Application.Common.Interfaces.Authentication;
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
            //CreateMap<User, UserSafetyResponse>();
            CreateMap<User, UserSafetyResponse>()
            .ForCtorParam("token", opt => opt.MapFrom(src => string.Empty));
            CreateMap<SearchUserRequest, User>();
        }
    }

}
