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
            var secretKey = context.Request.Headers["secretKey"].FirstOrDefault();

            //if (!await _userVerificationService.VerifyUserAsync(accessKey, secretKey))
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    await context.Response.WriteAsync("Unauthorized");
            //    return;
            //}

            if (!string.IsNullOrEmpty(accessKey))
            {
                var userInfo = await _userInfoServiceClient.GetUserInfoAsync(accessKey);
                if (userInfo != null)
                {
                    context.Items["UserId"] = userInfo.Id;
                }
            }
            await _next(context);
        }
    }
}
