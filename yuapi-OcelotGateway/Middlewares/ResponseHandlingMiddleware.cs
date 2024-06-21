using yuapi_OcelotGateway.Services;

namespace yuapi_OcelotGateway.Middlewares
{
    public class ResponseHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly InvokeCountServiceClient _invokeCountServiceClient;

        public ResponseHandlingMiddleware(RequestDelegate next, InvokeCountServiceClient invokeCountServiceClient)
        {
            _next = next;
            _invokeCountServiceClient = invokeCountServiceClient;
        }

        public async Task Invoke(HttpContext context)
        {
            // Capture the response body to read it
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Read the response body
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseText = new StreamReader(context.Response.Body).ReadToEnd();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // Call the InvokeCountServiceClient to update the count
                // Assuming you have the interfaceInfoId and userId from the request context or headers
                int interfaceInfoId = /* Retrieve from context or headers */ 11;
                int userId = /* Retrieve from context or headers */ 1;

                await _invokeCountServiceClient.UpdateCountAsync(interfaceInfoId, userId);

                // Copy the response back to the original body stream
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
