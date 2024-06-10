namespace yuapi_client_sdk.Utils
{
    public static class HeaderUtils
    {
        public static Dictionary<string, string> GetHeaderMap(string body, string accessKey, string secretKey)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("accessKey", accessKey);
            dict.Add("nonce", new Random().Next(1000, 10000).ToString()); // generate random number length 4
            dict.Add("body", body);
            dict.Add("timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
            dict.Add("sign", SignUtils.GenSign(body, secretKey));
            return dict;
        }
    }
}
