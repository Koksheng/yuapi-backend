﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Application.Users.Common
{
    public record UserSafetyResult(
        int Id,
        string userName,
        string userAccount,
        string userAvatar,
        int gender,
        int userRole,
        DateTime createTime,
        DateTime updateTime,
        string token);
}
