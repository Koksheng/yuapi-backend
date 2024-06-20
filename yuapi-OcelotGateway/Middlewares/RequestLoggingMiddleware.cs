namespace yuapi_OcelotGateway.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request ID: {context.Connection.Id}");
            _logger.LogInformation($"Request Path: {context.Request.Path}");
            _logger.LogInformation($"Request Method: {context.Request.Method}");
            _logger.LogInformation($"Request Source Address: {context.Connection.LocalIpAddress}");
            _logger.LogInformation($"Request Remote Address: {context.Connection.RemoteIpAddress}");
            await _next(context);
        }
    }
}
