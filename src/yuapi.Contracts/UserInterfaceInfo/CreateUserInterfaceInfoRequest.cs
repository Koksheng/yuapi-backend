using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Contracts.UserInterfaceInfo
{
    public record CreateUserInterfaceInfoRequest(
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
        int leftNum

        );
}
