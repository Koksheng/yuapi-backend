using AutoMapper;
using yuapi.Application.Common.Models;
using yuapi.Application.MappingProfiles.Common;
using yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Common;
using yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfoByPage;
using yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfos;
using yuapi.Contracts.Common;
using yuapi.Contracts.UserInterfaceInfo;
using yuapi.Domain.UserInterfaceInfoAggregate;
using yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects;

namespace yuapi.Application.MappingProfiles
{
    public class UserInterfaceInfoMappingProfile : Profile
    {
        public UserInterfaceInfoMappingProfile() 
        {
            // !!!!!!!!!!!!!! Type conversion configuration for UserInterfaceInfoId to int !!!!!!!!!!!!!!
            CreateMap<UserInterfaceInfoId, int>()
                .ConvertUsing(src => src.Value);

            // Create
            CreateMap<CreateUserInterfaceInfoRequest, CreateUserInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));
            CreateMap<CreateUserInterfaceInfoCommand, UserInterfaceInfo>();

            // Delete
            CreateMap<DeleteRequest, DeleteUserInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));

            // Update
            CreateMap<UpdateUserInterfaceInfoRequest, UpdateUserInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));
            CreateMap<UpdateUserInterfaceInfoCommand, UserInterfaceInfo>();

            // Get UserInterfaceInfo
            CreateMap<UserInterfaceInfo, UserInterfaceInfoSafetyResult>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.Value));
            CreateMap<UserInterfaceInfoSafetyResult, UserInterfaceInfoSafetyResponse>();

            // List UserInterfaceInfo
            CreateMap<QueryUserInterfaceInfoRequest, ListUserInterfaceInfosQuery>();
            CreateMap<ListUserInterfaceInfosQuery, UserInterfaceInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id > 0 ? UserInterfaceInfoId.Create(src.id) : null)); // Adjust based on how UserInterfaceInfoId is created

            // List UserInterfaceInfo By Page
            CreateMap<QueryUserInterfaceInfoRequest, ListUserInterfaceInfoByPageQuery>()
               .ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.current))
               .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.pageSize))
               .ForMember(dest => dest.SortField, opt => opt.MapFrom(src => src.sortField))
               .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.sortOrder));
            CreateMap<ListUserInterfaceInfoByPageQuery, UserInterfaceInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id > 0 ? UserInterfaceInfoId.Create(src.Id) : null)); // Adjust based on how UserInterfaceInfoId is created

            // Mapping for PaginatedList<UserInterfaceInfoSafetyResult> to PaginatedList<UserInterfaceInfoSafetyResponse>
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListTypeConverter<,>));

        }
    }
}
