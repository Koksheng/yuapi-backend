// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using yuapi_interface.Utils;

namespace yuapi_interface_console_app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8123");

            // GET request example
            var getNameResponse = await client.GetStringAsync("/api/name?name=yupi");
            Console.WriteLine(getNameResponse);

            // POST request example
            var postNameResponse = await client.PostAsync("/api/name?name=yupi", null);
            Console.WriteLine(await postNameResponse.Content.ReadAsStringAsync());

            // POST request with User example
            var user = new { username = "yupi" };
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            //client.DefaultRequestHeaders.Add("accessKey", "yupi");
            //client.DefaultRequestHeaders.Add("secretKey", "abcdefgh");
            // getHeaderMap() here
            var headers = GetHeaderMap(userJson);
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var postUserResponse = await client.PostAsync("/api/name/user", userContent);
            Console.WriteLine(await postUserResponse.Content.ReadAsStringAsync());

            Console.Read();
        }
        


        private static Dictionary<string, string> GetHeaderMap(string body)
        {
            //var dict = new Dictionary<string, string>
            //{
            //    { "accessKey", "yupi" },
            //    //{ "secretKey", "abcdefgh" },
            //    { "nonce", new Random().Next(1000000, 10000000).ToString() }, // generate random number length 7
            //    { "body", body },
            //    { "timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() }, // current time in seconds
            //    { "sign", genSign(dict, "abcdefgh")}
            //};
            var dict = new Dictionary<string, string>();
            dict.Add("accessKey", "yupi");
            dict.Add("nonce", new Random().Next(1000, 10000).ToString()); // generate random number length 4
            dict.Add("body", body);
            dict.Add("timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
            dict.Add("sign", SignUtils.GenSign(body, "abcdefgh"));
            

            return dict;
        }
    }
}
