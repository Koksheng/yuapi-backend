using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Users.Commands.Register;
using yuapi.Application.Users.Common;
using yuapi.Application.Users.Queries.Login;
using yuapi.Contracts.User;
using yuapi.Domain.Entities;

namespace yuapi.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // UserController
            CreateMap<UserRegisterRequest, UserRegisterCommand>();
            CreateMap<UserLoginRequest, UserLoginQuery>();

            //UserRegisterCommandHandler
            CreateMap<UserRegisterCommand, User>();
            CreateMap<User, UserSafetyResult>()
                .ForCtorParam("token", opt => opt.MapFrom(src => string.Empty));
            CreateMap<UserSafetyResult, UserSafetyResponse>();
            CreateMap<SearchUserRequest, User>();
        }
    }
}
