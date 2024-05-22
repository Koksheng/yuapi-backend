using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace yuapi.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string userAccount { get; set; }
        public string userAvatar { get; set; }
        public int gender { get; set; }
        public int userRole { get; set; }
        [JsonIgnore] // This property will be ignored during serialization and deserialization
        public string userPassword { get; set; }
        public string accessKey { get; set; } // 签名 accessKey
        public string secretKey { get; set; } // 签名 secretKey
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public bool isDelete { get; set; }
        //public string planetCode { get; set; }
        //public string phone { get; set; }

        //public string email { get; set; }
        //public int userStatus { get; set; }
    }
}
