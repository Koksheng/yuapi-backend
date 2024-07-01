using yuapi_client_sdk.Utils;
using yuapi_OcelotGateway.Services;

namespace yuapi_OcelotGateway.Middlewares
{
    public class UserVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserInfoServiceClient _userInfoServiceClient;

        public UserVerificationMiddleware(RequestDelegate next, UserInfoServiceClient userInfoServiceClient)
        {
            _next = next;
            _userInfoServiceClient = userInfoServiceClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessKey = context.Request.Headers["accessKey"].FirstOrDefault();
            //var secretKey = context.Request.Headers["secretKey"].FirstOrDefault();
            var nonce = context.Request.Headers["nonce"].FirstOrDefault();
            var timestamp = context.Request.Headers["timestamp"].FirstOrDefault();
            var sign = context.Request.Headers["sign"].FirstOrDefault();
            var body = context.Request.Headers["body"].FirstOrDefault();

            //context.Request.EnableBuffering();
            //var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            //context.Request.Body.Position = 0;
            /*
             * Issue encounter !!
             * It looks like the issue might be with how the request body is being read in the middleware. 
             * When you read the request body in your middleware, it can only be read once.
             * If you read the request body in the middleware and don't rewind the stream, subsequent middlewares or controllers that try to read the body will find it empty.
             * 
             * To ensure the request body can be read multiple times, you need to enable buffering and rewind the stream after reading it in your middleware. 
             */


            if (!long.TryParse(nonce, out long nonceValue) || nonceValue > 10000)
            {
                await HandleNoAuth(context);
                return;
            }

            if (!long.TryParse(timestamp, out long timestampValue))
            {
                await HandleNoAuth(context);
                return;
            }

            long currentTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            const long FIVE_MINUTES = 60 * 5;

            if ((currentTime - timestampValue) >= FIVE_MINUTES)
            {
                await HandleNoAuth(context);
                return;
            }

            if (!string.IsNullOrEmpty(accessKey))
            {
                var userInfo = await _userInfoServiceClient.GetUserInfoAsync(accessKey);
                if (userInfo != null)
                {
                    context.Items["UserId"] = userInfo.Id;

                    string serverSign = SignUtils.GenSign(body, userInfo.SecretKey);
                    if (sign == null || !sign.Equals(serverSign))
                    {
                        await HandleNoAuth(context);
                        return;
                    }
                }
                else
                {
                    await HandleNoAuth(context);
                    return;
                }
            }
            else
            {
                await HandleNoAuth(context);
                return;
            }
            
            await _next(context);
        }

        private async Task HandleNoAuth(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
