﻿using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Models;

namespace yuapi.Application.Common.Utils
{
    public class ResultUtils
    {
        public static BaseResponse<T> success<T>(T data)
        {
            return new BaseResponse<T>(0, data, "ok");
        }
        public static BaseResponse<T> error<T>(ErrorCode errorCode)
        {
            return new BaseResponse<T>(errorCode.Code, default(T), errorCode.Message, errorCode.Description);
        }
        public static BaseResponse<T> error<T>(int code, T data, string message, string description)
        {
            return new BaseResponse<T>(code, default(T), message, description);
        }
    }
}
