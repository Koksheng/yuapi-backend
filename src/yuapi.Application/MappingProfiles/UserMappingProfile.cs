using AutoMapper;
using yuapi.Application.Users.Commands.Register;
using yuapi.Application.Users.Common;
using yuapi.Application.Users.Queries.Login;
using yuapi.Contracts.User;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.UserAggregate.ValueObjects;
using yuapi.Application.Users.Queries.ListUserByPage;
using yuapi.Application.Common.Models;
using yuapi.Application.MappingProfiles.Common;
using yuapi.Application.Users.Commands.UpdateUser;

namespace yuapi.Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // !!!!!!!!!!!!!! Type conversion configuration for UserId to int !!!!!!!!!!!!!!
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

            // List User By Page
            CreateMap<QueryUserRequest, ListUserByPageQuery>()
               .ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.current))
               .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.pageSize))
               .ForMember(dest => dest.SortField, opt => opt.MapFrom(src => src.sortField))
               .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.sortOrder));
            CreateMap<ListUserByPageQuery, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id > 0 ? UserId.Create(src.Id) : null)); // Adjust based on how UserId is created
            CreateMap<UserSafetyResult, AdminPageUserSafetyResponse>();

            // Update
            CreateMap<UpdateUserRequest, UpdateUserCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));
            CreateMap<UpdateUserCommand, User>();

            // Mapping for PaginatedList<UserSafetyResult> to PaginatedList<UserSafetyResponse>
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListTypeConverter<,>));
        }
    }
}
