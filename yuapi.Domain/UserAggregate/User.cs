using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using yuapi.Domain.Common.Models;
using yuapi.Domain.UserAggregate.ValueObjects;

namespace yuapi.Domain.UserAggregate
{
    public sealed class User : AggregateRoot<UserId>
    {
        public string userName { get; set; }
        public string userAccount { get; set; }
        public string userAvatar { get; set; }
        public int gender { get; set; }
        public int userRole { get; set; }
        public string userPassword { get; set; }
        public string accessKey { get; set; } // 签名 accessKey
        public string secretKey { get; set; } // 签名 secretKey
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public bool isDelete { get; set; }

        private User(
        UserId userId,
        string userName,
        string userAccount,
        string userAvatar,
        int gender,
        int userRole,
        string userPassword,
        string accessKey,
        string secretKey,
        DateTime createTime,
        DateTime updateTime,
        bool isDelete)
        : base(userId)
        {
            userName = userName;
            userAccount = userAccount;
            userAvatar = userAvatar;
            gender = gender;
            userRole = userRole;
            userPassword = userPassword;
            accessKey = accessKey;
            secretKey = secretKey;
            createTime = createTime;
            updateTime = updateTime;
            isDelete = isDelete;
        }

        public static User Create(
            string userName,
            string userAccount,
            string userAvatar,
            int gender,
            int userRole,
            string userPassword,
            string accessKey,
            string secretKey)
        {
            return new(
                UserId.CreateUnique(),
                userName,
                userAccount,
                userAvatar,
                gender,
                userRole,
                userPassword,
                accessKey,
                secretKey,
                DateTime.UtcNow,
                DateTime.UtcNow,
                false);
        }
    }

    
}
