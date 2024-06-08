using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo
{
    public record DeleteInterfaceInfoCommand(int id, string userState) : IRequest<BaseResponse<int>>;
}
