using AutoMapper;
using yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo;
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
        }
    }
}
