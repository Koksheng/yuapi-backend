using AutoMapper;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Application.InterfaceInfos.Queries;
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
            CreateMap<GetInterfaceInfoRequest, GetInterfaceInfoByIdQuery>();
            CreateMap<InterfaceInfo, InterfaceInfoSafetyResult>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.Value));
            //CreateMap<GetInterfaceInfoByIdQuery, InterfaceInfoSafetyResult>();
            CreateMap<InterfaceInfoSafetyResult, InterfaceInfoSafetyResponse>();

        }
    }
}
