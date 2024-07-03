using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateFreeTrialUserInterfaceInfo
{
    public record UpdateFreeTrialUserInterfaceInfoCommand(
        int userId,
        int interfaceInfoId,
        int lockNum,
        string userState
        ) : IRequest<BaseResponse<int>>;
}
