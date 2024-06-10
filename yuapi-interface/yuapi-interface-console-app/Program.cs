// See https://aka.ms/new-console-template for more information
using yuapi_client_sdk.Model;
using yuapi_client_sdkyuapi_client_sdk.Client;

namespace yuapi_interface_console_client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Create an instance of HttpClient
            using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8123") };

            // Create an instance of YuApiClient
            var yuApiClient = new YuApiClient(httpClient);
            // Set the access key
            yuApiClient.SetAccessKey("yupi");
            // Set the secret key
            yuApiClient.SetSecretKey("abcdefgh");

            // Call the GetNameByGet method
            var getNameResult = await yuApiClient.GetNameByGet("yupi");
            Console.WriteLine($"GetNameByGet result: {getNameResult}");

            // Call the GetNameByPost method
            var postNameResult = await yuApiClient.GetNameByPost("yupi");
            Console.WriteLine($"GetNameByPost result: {postNameResult}");

            // Create a User object
            var user = new User("yupi");

            // Call the GetUsernameByPost method
            var postUserResult = await yuApiClient.GetUsernameByPost(user);
            Console.WriteLine($"GetUsernameByPost result: {postUserResult}");

            Console.Read();
        }
    }
}
