//using AutoMapper;
//using yuapi.Application.Common.Interfaces.Authentication;
//using yuapi.Application.Users.Commands.Register;
//using yuapi.Application.Users.Common;
//using yuapi.Application.Users.Queries.Login;
//using yuapi.Contracts.InterfaceInfo;
//using yuapi.Contracts.User;
//using yuapi.Domain.Entities;

//namespace yuapi.Application.MappingProfiles
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<InterfaceInfoAddRequest, InterfaceInfo>();
//            // UserController
//            CreateMap<UserRegisterRequest, UserRegisterCommand>();
//            CreateMap<UserLoginRequest, UserLoginQuery>();

//            //UserRegisterCommandHandler
//            CreateMap<UserRegisterCommand, User>();


//            //CreateMap<User, UserSafetyResponse>();
//            CreateMap<User, UserSafetyResult>()
//            .ForCtorParam("token", opt => opt.MapFrom(src => string.Empty));
//            CreateMap<UserSafetyResult, UserSafetyResponse>();
//            CreateMap<SearchUserRequest, User>();
//        }
//    }

//}
