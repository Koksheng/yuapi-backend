using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Domain.Entities
{
    public class InterfaceInfo
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string requestHeader { get; set; }
        public string responseHeader { get; set; }
        public string userId { get; set; }
        public int status { get; set; }
        public string method { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public int isDelete { get; set; }
    }
}
