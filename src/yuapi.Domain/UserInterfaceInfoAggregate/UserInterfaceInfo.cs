using yuapi.Domain.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;
using yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects;

namespace yuapi.Domain.UserInterfaceInfoAggregate
{
    public sealed class UserInterfaceInfo : AggregateRoot<UserInterfaceInfoId, int>
    {
        public int userId { get; set; }
        public int interfaceInfoId { get; set; }
        public int totalNum { get; set; }
        public int leftNum { get; set; }
        public int status { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public int isDelete { get; set; }

        private UserInterfaceInfo(
            UserInterfaceInfoId userInterfaceInfoId,
            int userId, 
            int interfaceInfoId, 
            int totalNum, 
            int leftNum, 
            int status, 
            DateTime createTime, 
            DateTime updateTime, 
            int isDelete)
            : base(userInterfaceInfoId)
        {
            this.userId = userId;
            this.interfaceInfoId = interfaceInfoId;
            this.totalNum = totalNum;
            this.leftNum = leftNum;
            this.status = status;
            this.createTime = createTime;
            this.updateTime = updateTime;
            this.isDelete = isDelete;
        }

        public static UserInterfaceInfo Create(
            int userId,
            int interfaceInfoId,
            int totalNum,
            int leftNum,
            int status
            )
        {
            return new(
                null,   // EF Core will set this value
                userId,
                interfaceInfoId,
                totalNum,
                leftNum,
                status,
                DateTime.UtcNow,
                DateTime.UtcNow,
                0);
        }
        // Private parameterless constructor for EF Core
        private UserInterfaceInfo() : base(null)
        {
            // EF Core requires an empty constructor for materialization
        }
    }
}
