﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Contracts.InterfaceInfo
{
    public record CreateInterfaceInfoRequest(
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
     * 请求参数
     */
    string? requestParams,
    /**
     * 请求头
     */
    string requestHeader,

    /**
     * 响应头
     */
    string responseHeader,

    /**
     * 接口状态（0-关闭，1-开启）
     */
    //int status,

    /**
     * 请求类型
     */
    string method

    

    );
}
