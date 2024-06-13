using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo
{
    public record DeleteUserInterfaceInfoCommand(int id, string userState) : IRequest<BaseResponse<int>>;
}
