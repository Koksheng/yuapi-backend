using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.Menus.Commands.CreateMenu;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Contracts.Menus;

namespace yuapi.Application.MappingProfiles
{
    public class MenuMappingProfile : Profile
    {
        public MenuMappingProfile()
        {
            CreateMap< CreateMenuRequest, CreateMenuCommand>()
                .ForCtorParam("UserId", opt => opt.MapFrom(src => string.Empty));
        }
    }
}
