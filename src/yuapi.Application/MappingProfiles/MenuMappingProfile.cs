using AutoMapper;
using yuapi.Application.Menus.Commands.CreateMenu;
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
