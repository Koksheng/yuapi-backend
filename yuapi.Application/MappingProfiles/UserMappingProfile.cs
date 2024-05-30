using AutoMapper;
using yuapi.Application.Users.Commands.Register;
using yuapi.Application.Users.Common;
using yuapi.Application.Users.Queries.Login;
using yuapi.Contracts.User;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.UserAggregate.ValueObjects;

namespace yuapi.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Type conversion configuration for UserId to int
            CreateMap<UserId, int>().ConvertUsing(src => src.Value);

            // UserController
            CreateMap<UserRegisterRequest, UserRegisterCommand>();
            CreateMap<UserLoginRequest, UserLoginQuery>();

            //UserRegisterCommandHandler
            CreateMap<UserRegisterCommand, User>();
            CreateMap<User, UserSafetyResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))  // Mapping UserId.Value to Id
                .ForCtorParam("token", opt => opt.MapFrom(src => string.Empty));
            CreateMap<UserSafetyResult, UserSafetyResponse>();
            CreateMap<SearchUserRequest, User>();
        }
    }
}
