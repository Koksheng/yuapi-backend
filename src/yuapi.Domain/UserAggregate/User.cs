using yuapi.Domain.Common.Models;
using yuapi.Domain.UserAggregate.Events;
using yuapi.Domain.UserAggregate.ValueObjects;

namespace yuapi.Domain.UserAggregate
{
    public sealed class User : AggregateRoot<UserId, int>
    {
        public string userName { get; set; }
        public string userAccount { get; set; }
        public string userAvatar { get; set; }
        public int? gender { get; set; }
        public string userRole { get; set; }
        public string userPassword { get; set; }
        public string accessKey { get; set; } // 签名 accessKey
        public string secretKey { get; set; } // 签名 secretKey
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public bool? isDelete { get; set; }

        private User(
            UserId userId,
            string userName,
            string userAccount,
            string userAvatar,
            int? gender,
            string userRole,
            string userPassword,
            string accessKey,
            string secretKey,
            DateTime createTime,
            DateTime updateTime,
            bool? isDelete)
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
            string userRole,
            string userPassword,
            string accessKey,
            string secretKey)
        {
            var user = new User(
                null,  // EF Core will set this value
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
            user.AddDomainEvent(new UserCreated(user));
            return user;
        }

        // Private parameterless constructor for EF Core
        private User() : base(null)
        {
            // EF Core requires an empty constructor for materialization
        }
    }

    
}
