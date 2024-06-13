using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo
{
    public record UpdateUserInterfaceInfoCommand(
        int id,

        /**
         * 总调用次数
         */
        int totalNum,

        /**
         * 剩余调用次数
         */
        int leftNum,

        /**
         * 0-正常，1-禁用
         */
        int status,

        string userState


        ) : IRequest<BaseResponse<int>>;
}
