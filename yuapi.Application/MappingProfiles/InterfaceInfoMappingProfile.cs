using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Entities;

namespace yuapi.Application.MappingProfiles
{
    public class InterfaceInfoMappingProfile : Profile
    {
        public InterfaceInfoMappingProfile()
        {
            // InterfaceInfo mappings
            CreateMap<InterfaceInfoAddRequest, InterfaceInfo>();
        }
    }
}
