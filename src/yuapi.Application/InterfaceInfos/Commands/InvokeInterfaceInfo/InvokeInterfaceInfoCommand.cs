using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.InterfaceInfos.Commands.InvokeInterfaceInfo
{
    public record InvokeInterfaceInfoCommand(
        int id,
        string userRequestParams,
        string userState
        ) : IRequest<BaseResponse<string>>;
}
