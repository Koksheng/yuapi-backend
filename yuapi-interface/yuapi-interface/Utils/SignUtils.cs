using System.Security.Cryptography;
using System.Text;

namespace yuapi_interface.Utils
{
    public class SignUtils
    {
        // Hashes the password using HMAC-SHA256 with a secret key
        public static string HashPasswordWithKey(string password, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string GenSign(string body, string secretKey)
        {
            string hashedSignature = HashPasswordWithKey(body, secretKey);
            return hashedSignature;
        }
    }
}
