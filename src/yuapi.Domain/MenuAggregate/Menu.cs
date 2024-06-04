using yuapi.Domain.Common.Models;
using yuapi.Domain.MenuAggregate.Events;
using yuapi.Domain.MenuAggregate.ValueObjects;

namespace yuapi.Domain.MenuAggregate
{

    public sealed class Menu : AggregateRoot<MenuId, int>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Url { get; private set; }
        public string RequestHeader { get; private set; }
        public string ResponseHeader { get; private set; }
        public string UserId { get; private set; }
        public int Status { get; private set; }
        public string Method { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public int IsDelete { get; private set; }


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
            var menu = new Menu(
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

            menu.AddDomainEvent(new MenuCreated(menu));

            return menu;
        }


        // Private parameterless constructor for EF Core
        private Menu() : base(MenuId.CreateUnique())
        {
            // EF Core requires an empty constructor for materialization
        }
    }


}
