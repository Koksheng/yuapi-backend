using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Contracts.InterfaceInfo
{
    public record InterfaceInfoAddRequest(
     /**
     * 接口名称
     */
    string name,

    /**
     * 接口描述
     */
    string description,

    /**
     * 接口地址
     */
    string url,

    /**
     * 请求头
     */
    string requestHeader,

    /**
     * 响应头
     */
    string responseHeader,
    int status,

    /**
     * 请求类型
     */
    string method

    /**
     * 请求参数
     */
    //string requestParams,

    );
}
