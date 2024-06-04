using AutoMapper;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.MappingProfiles
{
    public class InterfaceInfoMappingProfile : Profile
    {
        public InterfaceInfoMappingProfile()
        {
            // InterfaceInfo mappings
            CreateMap<CreateInterfaceInfoRequest, CreateInterfaceInfoCommand>()
                .ForCtorParam("userId", opt => opt.MapFrom(src => string.Empty));
            CreateMap<CreateInterfaceInfoCommand, InterfaceInfo>();
        }
    }
}
