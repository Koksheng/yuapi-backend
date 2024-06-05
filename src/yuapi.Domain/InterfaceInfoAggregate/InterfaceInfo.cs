using yuapi.Domain.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;

namespace yuapi.Domain.InterfaceInfoAggregate
{
    public sealed class InterfaceInfo : AggregateRoot<InterfaceInfoId, int>
    {
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string requestParam{ get; set; }
        public string requestHeader { get; set; }
        public string responseHeader { get; set; }
        public int status { get; set; }
        public string method { get; set; }
        public int userId { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public int isDelete { get; set; }

        private InterfaceInfo(
            InterfaceInfoId interfaceInfoId,
            string name,
            string description,
            string url,
            string requestParam,
            string requestHeader,
            string responseHeader,
            int status,
            string method,
            int userId,
            DateTime createTime,
            DateTime updateTime,
            int isDelete)
            : base(interfaceInfoId)
        {
            name = name;
            description = description;
            url = url;
            requestParam = requestParam;
            requestHeader = requestHeader;
            responseHeader = responseHeader;
            status = status;
            method = method;
            userId = userId;
            createTime = createTime;
            updateTime = updateTime;
            isDelete = isDelete;
        }

        public static InterfaceInfo Create(
            string name,
            string description,
            string url,
            string requestParam,
            string requestHeader,
            string responseHeader,
            int status,
            string method,
            int userId)
        {
            return new(
                null,  // EF Core will set this value
                name,
                description,
                url,
                requestParam,
                requestHeader,
                responseHeader,
                status,
                method,
                userId,
                DateTime.UtcNow,
                DateTime.UtcNow,
                0);
        }
        // Private parameterless constructor for EF Core
        private InterfaceInfo() : base(null)
        {
            // EF Core requires an empty constructor for materialization
        }
    }
}