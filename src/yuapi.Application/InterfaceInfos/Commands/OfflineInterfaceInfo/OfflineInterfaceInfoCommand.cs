using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.InterfaceInfos.Commands.OfflineInterfaceInfo
{
    public record OfflineInterfaceInfoCommand(int id, string userState) : IRequest<BaseResponse<bool>>;
}
