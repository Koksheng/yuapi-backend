using yuapi_OcelotGateway.Services;

namespace yuapi_OcelotGateway.Middlewares
{
    public class AccessControlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _whitelist = new[] { "127.0.0.1" };
        private const string INTERFACE_HOST = "http://localhost:8123";
        private readonly InterfaceInfoServiceClient _interfaceInfoServiceClient;

        public AccessControlMiddleware(RequestDelegate next, InterfaceInfoServiceClient interfaceInfoServiceClient)
        {
            _next = next;
            _interfaceInfoServiceClient = interfaceInfoServiceClient;
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
            var path = INTERFACE_HOST + context.Request.Path.ToString();
            var method = context.Request.Method;

            var interfaceInfo = await _interfaceInfoServiceClient.GetInterfaceInfoAsync(path, method);

            if (interfaceInfo != null)
            {
                context.Items["InterfaceId"] = interfaceInfo.Id;
            }
            await _next(context);
        }
    }
}
