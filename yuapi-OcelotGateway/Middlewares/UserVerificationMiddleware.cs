using yuapi_OcelotGateway.Services;

namespace yuapi_OcelotGateway.Middlewares
{
    public class UserVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserVerificationService _userVerificationService;

        public UserVerificationMiddleware(RequestDelegate next, IUserVerificationService userVerificationService)
        {
            _next = next;
            _userVerificationService = userVerificationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessKey = context.Request.Headers["accessKey"].FirstOrDefault();
            var secretKey = context.Request.Headers["secretKey"].FirstOrDefault();

            if (!await _userVerificationService.VerifyUserAsync(accessKey, secretKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await _next(context);
        }
    }
}
