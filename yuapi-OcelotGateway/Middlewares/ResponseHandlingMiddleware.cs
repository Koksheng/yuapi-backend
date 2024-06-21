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

                if (context.Items.TryGetValue("InterfaceId", out var interfaceIdObj) &&
                    context.Items.TryGetValue("UserId", out var userIdObj) &&
                    int.TryParse(interfaceIdObj?.ToString(), out var interfaceInfoId) &&
                    int.TryParse(userIdObj?.ToString(), out var userId))
                {
                    await _invokeCountServiceClient.UpdateCountAsync(interfaceInfoId, userId);
                }
                else
                {
                    // Handle the error case where the items were not found or not valid integers
                    throw new InvalidOperationException("Invalid context items: InterfaceId or UserId.");
                }
                // Copy the response back to the original body stream
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
