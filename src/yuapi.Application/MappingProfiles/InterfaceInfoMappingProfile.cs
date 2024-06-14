using AutoMapper;
using yuapi.Application.Common.Models;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.InvokeInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.OfflineInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos;
using yuapi.Application.MappingProfiles.Common;
using yuapi.Contracts.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;

namespace yuapi.Application.MappingProfiles
{
    public class InterfaceInfoMappingProfile : Profile
    {
        public InterfaceInfoMappingProfile()
        {
            // !!!!!!!!!!!!!! Type conversion configuration for InterfaceInfoId to int !!!!!!!!!!!!!!
            CreateMap<InterfaceInfoId, int>()
                .ConvertUsing(src => src.Value);

            // Create
            CreateMap<CreateInterfaceInfoRequest, CreateInterfaceInfoCommand>()
                .ForCtorParam("status", opt => opt.MapFrom(src => 1))
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));
            CreateMap<CreateInterfaceInfoCommand, InterfaceInfo>();

            // Delete
            CreateMap<DeleteInterfaceInfoRequest, DeleteInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));

            // Update
            CreateMap<UpdateInterfaceInfoRequest, UpdateInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));
            CreateMap<UpdateInterfaceInfoCommand, InterfaceInfo>();

            // Get InterfaceInfo
            CreateMap<InterfaceInfo, InterfaceInfoSafetyResult>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.Value));
            CreateMap<InterfaceInfoSafetyResult, InterfaceInfoSafetyResponse>();

            // List InterfaceInfos
            CreateMap<QueryInterfaceInfoRequest, ListInterfaceInfosQuery>();
            CreateMap<ListInterfaceInfosQuery, InterfaceInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id > 0 ? InterfaceInfoId.Create(src.id) : null)); // Adjust based on how InterfaceInfoId is created

            // List InterfaceInfo By Page
            CreateMap<QueryInterfaceInfoRequest, ListInterfaceInfoByPageQuery>()
               .ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.current))
               .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.pageSize))
               .ForMember(dest => dest.SortField, opt => opt.MapFrom(src => src.sortField))
               .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.sortOrder));
               //.ForMember(dest => dest.Current, opt => opt.MapFrom(src => src.PageRequest.Current))
               //.ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageRequest.PageSize))
               //.ForMember(dest => dest.SortField, opt => opt.MapFrom(src => src.PageRequest.SortField))
               //.ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.PageRequest.SortOrder));
            CreateMap<ListInterfaceInfoByPageQuery, InterfaceInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id > 0 ? InterfaceInfoId.Create(src.Id) : null)); // Adjust based on how InterfaceInfoId is created

            // Mapping for PaginatedList<InterfaceInfoSafetyResult> to PaginatedList<InterfaceInfoSafetyResponse>
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListTypeConverter<,>));

            // Online Interface Info
            CreateMap<IdRequest, OnlineInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));

            // Offline Interface Info
            CreateMap<IdRequest, OfflineInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));

            // Invoke Interface Info
            CreateMap<InvokeInterfaceInfoRequest, InvokeInterfaceInfoCommand>()
                .ForCtorParam("userState", opt => opt.MapFrom(src => string.Empty));

        }
    }

}
