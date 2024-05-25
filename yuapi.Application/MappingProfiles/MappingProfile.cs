using AutoMapper;
using yuapi.Application.Common.Interfaces.Authentication;
using yuapi.Application.Users.Commands.Register;
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
            //CreateMap<UserRegisterRequest, User>();
            CreateMap<UserRegisterCommand, User>();
            CreateMap<User, UserSafetyResponse>()
            .ForCtorParam("token", opt => opt.MapFrom(src => string.Empty));
            CreateMap<SearchUserRequest, User>();
        }
    }

}
