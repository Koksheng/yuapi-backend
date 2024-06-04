using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Common;

namespace yuapi.Application.Menus.Commands.CreateMenu
{
    public record CreateMenuCommand(
    /**
     * 接口名称
     */
    string Name,

    /**
     * 接口描述
     */
    string Description,

    /**
     * 接口地址
     */
    string Url,

    /**
     * 请求头
     */
    string RequestHeader,

    /**
     * 响应头
     */
    string ResponseHeader,


    string UserId,
    int Status,

    /**
     * 请求类型
     */
    string Method

    /**
     * 请求参数
     */
    //string requestParams
        ) : IRequest<BaseResponse<int>>;
}
