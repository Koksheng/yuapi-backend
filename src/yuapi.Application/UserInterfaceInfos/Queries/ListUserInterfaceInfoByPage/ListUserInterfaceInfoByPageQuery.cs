using MediatR;
using yuapi.Application.Common.Models;
using yuapi.Application.UserInterfaceInfos.Common;
using yuapi.Contracts.Common;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfoByPage
{
    public class ListUserInterfaceInfoByPageQuery : PageRequest, IRequest<PaginatedList<UserInterfaceInfoSafetyResult>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InterfaceInfoId { get; set; }
        public int TotalNum { get; set; }
        public int LeftNum { get; set; }
        public int Status { get; set; }

        // Parameterless constructor
        public ListUserInterfaceInfoByPageQuery() { }

        // Optional constructor with parameters
        public ListUserInterfaceInfoByPageQuery(
            int id,
            int userId,
            int interfaceInfoId,
            int totalNum,
            int leftNum,
            int status, 
            int current, int pageSize,
            string sortField, string sortOrder)
        {
            Id = id;
            UserId = userId;
            InterfaceInfoId = interfaceInfoId;
            TotalNum = totalNum;
            LeftNum = leftNum;
            Status = status;
            Current = current;
            PageSize = pageSize;
            SortField = sortField;
            SortOrder = sortOrder;
        }
    }
}
