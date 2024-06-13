using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Models;

namespace yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo
{
    public record CreateUserInterfaceInfoCommand(
        /**
         * 调用用户 id
         */
        int userId,
        /**
         * 接口 id
         */
        int interfaceInfoId,
        /**
         * 总调用次数
         */
        int totalNum,
        /**
         * 剩余调用次数
         */
        int leftNum,
        string userState

        ) : IRequest<BaseResponse<int>>;
}
