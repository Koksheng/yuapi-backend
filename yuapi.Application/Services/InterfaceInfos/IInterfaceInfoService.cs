using yuapi.Domain.Common;
using yuapi.Domain.Entities;

namespace yuapi.Application.Services.InterfaceInfos
{
    public interface IInterfaceInfoService
    {
        Task<BaseResponse<int>> CreateInterfaceInfo(InterfaceInfo interfaceInfo);
        void ValidateInterfaceInfo(InterfaceInfo interfaceInfo);
    }
}
