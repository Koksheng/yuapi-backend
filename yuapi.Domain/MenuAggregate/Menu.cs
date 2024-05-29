using yuapi.Domain.Common.Models;
using yuapi.Domain.MenuAggregate.ValueObjects;

namespace yuapi.Domain.MenuAggregate
{

    public sealed class Menu : AggregateRoot<MenuId>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string RequestHeader { get; set; }
        public string ResponseHeader { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
        public string Method { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int IsDelete { get; set; }

        private Menu(
            MenuId menuId,
            string name,
            string description,
            string url,
            string requestHeader,
            string responseHeader,
            string userId,
            int status,
            string method,
            DateTime createTime,
            DateTime updateTime,
            int isDelete)
            : base(menuId)
        {
            Name = name;
            Description = description;
            Url = url;
            RequestHeader = requestHeader;
            ResponseHeader = responseHeader;
            UserId = userId;
            Status = status;
            Method = method;
            CreateTime = createTime;
            UpdateTime = updateTime;
            IsDelete = isDelete;
        }

        public static Menu Create(
                string name,
                string description,
                string url,
                string requestHeader,
                string responseHeader,
                string userId,
                int status,
                string method)
        {
            return new(
                MenuId.CreateUnique(),
                name,
                description,
                url,
                requestHeader,
                responseHeader,
                userId,
                status,
                method,
                DateTime.UtcNow,
                DateTime.UtcNow,
                0);
        }

    }

    
}
