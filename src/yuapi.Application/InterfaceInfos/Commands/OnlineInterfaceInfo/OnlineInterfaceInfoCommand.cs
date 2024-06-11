using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo
{
    public record OnlineInterfaceInfoCommand(int id, string userState) : IRequest<BaseResponse<bool>>;
}
