﻿namespace yuapi.Application.Users.Common
{
    public record UserSafetyResult(
        int Id,
        string userName,
        string userAccount,
        string userAvatar,
        int gender,
        string userRole,
        string accessKey,
        string secretKey,
        DateTime createTime,
        DateTime updateTime,
        int isDelete,
        string token);
}
