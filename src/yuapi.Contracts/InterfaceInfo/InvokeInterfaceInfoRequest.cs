﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Contracts.InterfaceInfo
{
    public record InvokeInterfaceInfoRequest(
        int id,
        string? userRequestParams
        );
}
