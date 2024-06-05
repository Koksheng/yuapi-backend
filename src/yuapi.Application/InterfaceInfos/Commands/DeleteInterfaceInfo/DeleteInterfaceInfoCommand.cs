using MediatR;
using yuapi.Domain.Common;

namespace yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo
{
    public record DeleteInterfaceInfoCommand(int id, string userState) : IRequest<BaseResponse<int>>;
}
