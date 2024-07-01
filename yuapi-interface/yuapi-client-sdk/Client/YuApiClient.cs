using System.Text;
using yuapi_client_sdk.Model;
using yuapi_client_sdk.Utils;
using Newtonsoft.Json;

namespace yuapi_client_sdkyuapi_client_sdk.Client
{
    public class YuApiClient
    {
        private readonly HttpClient _httpClient;
        private string _accessKey;
        private string _secretKey;

        public YuApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to set the access key
        public void SetAccessKey(string accessKey)
        {
            _accessKey = accessKey;
        }

        // Method to set the secret key
        public void SetSecretKey(string secretKey)
        {
            _secretKey = secretKey;
        }

        public async Task<string> InvokeAsync(string methodName, params object[] parameters)
        {
            var method = GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (method == null)
            {
                throw new ArgumentException($"Method {methodName} not found.");
            }

            //var userJson = JsonConvert.SerializeObject(parameters);
            var userJson = JsonConvert.SerializeObject(parameters.Length == 1 ? parameters[0] : parameters);
            var headers = HeaderUtils.GetHeaderMap(userJson, _accessKey, _secretKey);
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var task = (Task<string>)method.Invoke(this, parameters);
            return await task;
        }

        public async Task<string> GetNameByGet(string name)
        {
            var response = await _httpClient.GetAsync($"/api/name?name={name}");
            //Console.WriteLine(response);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNameByPost(string name)
        {
            //var content = new StringContent($"name={name}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync($"/api/name?name={name}", null);
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetUsernameByPost(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            //var headers = HeaderUtils.GetHeaderMap(userJson, _accessKey, _secretKey);
            //foreach (var header in headers)
            //{
            //    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            //}

            var postUserResponse = await _httpClient.PostAsync("/api/name/user", userContent);
            //Console.WriteLine(await postUserResponse.Content.ReadAsStringAsync());
            return await postUserResponse.Content.ReadAsStringAsync();

        }
    }

    
}
