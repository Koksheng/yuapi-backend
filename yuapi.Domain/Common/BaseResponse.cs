namespace yuapi.Domain.Common
{
    public record BaseResponse<T>(int code, T data, string message = "", string description = "")
    {

    };
}
