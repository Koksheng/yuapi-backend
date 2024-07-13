# YuApiClient SDK #
The YuApiClient SDK is a .NET library designed to facilitate API requests through a secure and managed pipeline. It integrates seamlessly with the Ocelot API Gateway, allowing for request logging, access control, user verification, quota checking, and response handling.

Features
Request Logging: Automatically logs all requests for monitoring and debugging purposes.
Access Control: Verifies interface information to ensure secure API access.
User Verification: Authenticates users before processing their requests.
Quota Checking: Manages and enforces invocation quotas for API endpoints.
Response Handling: Processes and handles responses from API endpoints.
Installation
To install the YuApiClient SDK, use the NuGet Package Manager Console:

bash
Copy code
Install-Package YuApiClient
Or, you can add it to your project file:

xml
Copy code
<PackageReference Include="YuApiClient" Version="1.0.0" />
Usage
Initialization
First, initialize the YuApiClient with an HttpClient:

csharp
Copy code
using YuApiClient;

var httpClient = new HttpClient { BaseAddress = new Uri("http://127.0.0.1:8090") };
var yuApiClient = new YuApiClient(httpClient);
Setting Access and Secret Keys
Set the access and secret keys for authentication:

csharp
Copy code
yuApiClient.SetAccessKey("your-access-key");
yuApiClient.SetSecretKey("your-secret-key");
Invoking an API Method
You can invoke API methods using the InvokeAsync method:

csharp
Copy code
try
{
    var result = await yuApiClient.InvokeAsync("GetUsernameByPost", "{\"username\":\"exampleUser\"}");
    Console.WriteLine(result);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
Example: Fetching a Random Anime Image
To fetch a random anime image:

csharp
Copy code
try
{
    var imageBytes = await yuApiClient.InvokeAsync("GetRandomAnimeImage", string.Empty) as byte[];
    if (imageBytes != null)
    {
        File.WriteAllBytes("random_anime_image.jpg", imageBytes);
        Console.WriteLine("Image saved successfully.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
Methods
SetAccessKey(string accessKey)
Sets the access key for API authentication.

SetSecretKey(string secretKey)
Sets the secret key for API authentication.

InvokeAsync(string methodName, string serializedParams)
Invokes the specified API method with the given parameters. Returns the result as an object, which should be cast to the appropriate type.

GetRandomAnimeImage()
Fetches a random anime image as a byte array.

Middleware Pipeline
Requests made through the YuApiClient SDK are processed by the following middleware components:

RequestLoggingMiddleware: Logs each request for audit and debugging purposes.
AccessControlMiddleware: Ensures the request has proper access permissions.
UserVerificationMiddleware: Authenticates the user making the request.
QuotaCheckMiddleware: Ensures the user has not exceeded their request quota.
ResponseHandlingMiddleware: Processes and formats the response before returning it to the client.
Contributing
If you'd like to contribute to the YuApiClient SDK, please fork the repository and submit a pull request.

License
This project is licensed under the MIT License.