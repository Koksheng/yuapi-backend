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

        public async Task<object> InvokeAsync(string methodName, string serializedParams)
        {
            object[] parameters = methodName switch
            {
                "GetNameByGet" => new object[] { serializedParams },
                "GetUsernameByPost" => new object[] { JsonConvert.DeserializeObject<User>(serializedParams) },
                "GetRandomAnimeImage" => new object[] {  },
                _ => throw new ArgumentException($"Unsupported method {methodName}.")
            };

            var userJson = JsonConvert.SerializeObject(parameters);
            var headers = HeaderUtils.GetHeaderMap(userJson, _accessKey, _secretKey);
            foreach (var header in headers)
            {
                if (_httpClient.DefaultRequestHeaders.Contains(header.Key))
                {
                    _httpClient.DefaultRequestHeaders.Remove(header.Key);
                }
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var method = GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (method == null)
            {
                throw new ArgumentException($"Method {methodName} not found.");
            }
            try
            {
                var task = (Task)method.Invoke(this, parameters);
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty?.GetValue(task);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetNameByGet(string name)
        {
            var response = await _httpClient.GetAsync($"/api/name?name={name}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNameByPost(string name)
        {
            var response = await _httpClient.PostAsync($"/api/name?name={name}", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetUsernameByPost(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            var postUserResponse = await _httpClient.PostAsync("/api/name/user", userContent);
            if (!postUserResponse.IsSuccessStatusCode)
            {
                var errorContent = await postUserResponse.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await postUserResponse.Content.ReadAsStringAsync();

        }

        public async Task<byte[]> GetRandomAnimeImage()
        {
            var response = await _httpClient.GetAsync("https://pic.re/image");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
            return await response.Content.ReadAsByteArrayAsync();
        }
    }

    
}
