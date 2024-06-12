using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Application.Services.Common
{
    public class EncryptionService
    {
        private const string SALT = "yuapi_backend";

        // Hashes the password using HMAC-SHA256 with a secret key
        public static string HashPasswordWithKey(string password, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string EncryptPassword(string userPassword)
        {
            string hashedPassword = HashPasswordWithKey(userPassword, SALT);
            return hashedPassword;
        }

        public static bool VerifyPassword(string userPassword, string passwordToBeVerify)
        {
            string hashedPassword = EncryptPassword(passwordToBeVerify);
            if (userPassword == hashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string EncryptAccessKey(string userAccount)
        {
            string randomNumbers = GenerateRandomNumbers(5);
            string rawData = SALT + userAccount + randomNumbers;
            string hashedData = ComputeMd5Hash(rawData);
            return hashedData;
        }

        public static string EncryptSecretKey(string userAccount)
        {
            string randomNumbers = GenerateRandomNumbers(8);
            string rawData = SALT + userAccount + randomNumbers;
            string hashedData = ComputeMd5Hash(rawData);
            return hashedData;
        }

        private static string GenerateRandomNumbers(int length)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(random.Next(0, 10));
            }
            return builder.ToString();
        }

        private static string ComputeMd5Hash(string rawData)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(rawData);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
