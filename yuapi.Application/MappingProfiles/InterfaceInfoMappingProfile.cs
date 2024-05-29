using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.Users.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Entities;

namespace yuapi.Application.MappingProfiles
{
    public class InterfaceInfoMappingProfile : Profile
    {
        public InterfaceInfoMappingProfile()
        {
            // InterfaceInfo mappings
            //CreateMap<InterfaceInfoAddRequest, InterfaceInfo>();
            CreateMap<InterfaceInfoAddRequest, CreateInterfaceInfoCommand>()
                .ForCtorParam("userId", opt => opt.MapFrom(src => string.Empty));
        }
    }
}
