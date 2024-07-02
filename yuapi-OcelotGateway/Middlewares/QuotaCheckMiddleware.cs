using yuapi_OcelotGateway.Services;

namespace yuapi_OcelotGateway.Middlewares
{
    public class QuotaCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly QuotaCheckServiceClient _quotaCheckServiceClient;

        public QuotaCheckMiddleware(RequestDelegate next, QuotaCheckServiceClient quotaCheckServiceClient)
        {
            _next = next;
            _quotaCheckServiceClient = quotaCheckServiceClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Items.TryGetValue("InterfaceId", out var interfaceIdObj) &&
                context.Items.TryGetValue("UserId", out var userIdObj) &&
                int.TryParse(interfaceIdObj?.ToString(), out var interfaceId) &&
                int.TryParse(userIdObj?.ToString(), out var userId))
            {
                var quotaCheckReply = await _quotaCheckServiceClient.GetUserInterfaceInfoAsync(interfaceId, userId);

                if (quotaCheckReply.Success == false)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("User interface info not found.");
                    return;
                }

                if (quotaCheckReply.UserInterfaceInfo.LeftNum <= 0)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("No remaining quota to call/invoke anymore.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
