namespace yuapi_OcelotGateway.Middlewares
{
    public class AccessControlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _whitelist = new[] { "127.0.0.1" };

        public AccessControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();
            if (remoteIp != null && !_whitelist.Contains(remoteIp))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden");
                return;
            }
            await _next(context);
        }
    }
}
