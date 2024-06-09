using System.Text.Json;
using System.Text;
using yuapi_interface.Model;

namespace yuapi_interface.Client
{
    public class YuApiClient
    {
        private readonly HttpClient _httpClient;

        public YuApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetNameByGet(string name)
        {
            var response = await _httpClient.GetAsync($"/api/name?name={name}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNameByPost(string name)
        {
            var content = new StringContent($"name={name}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync("/api/name", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetUsernameByPost(User user)
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/name/user", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
