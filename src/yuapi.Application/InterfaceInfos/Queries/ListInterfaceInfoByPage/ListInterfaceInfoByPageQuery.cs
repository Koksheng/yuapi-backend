using MediatR;
using yuapi.Application.Common.Models;
using yuapi.Application.InterfaceInfos.Common;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage
{
    public class ListInterfaceInfoByPageQuery : IRequest<PaginatedList<InterfaceInfoSafetyResult>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string RequestHeader { get; set; }
        public string ResponseHeader { get; set; }
        public int Status { get; set; }
        public string Method { get; set; }
        public int UserId { get; set; }
        public int Current { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }

        // Parameterless constructor
        public ListInterfaceInfoByPageQuery() { }

        // Optional constructor with parameters
        public ListInterfaceInfoByPageQuery(
            int id, string name, string description, string url,
            string requestHeader, string responseHeader, int status,
            string method, int userId, int current, int pageSize,
            string sortField, string sortOrder)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            RequestHeader = requestHeader;
            ResponseHeader = responseHeader;
            Status = status;
            Method = method;
            UserId = userId;
            Current = current;
            PageSize = pageSize;
            SortField = sortField;
            SortOrder = sortOrder;
        }
    }
    //public record ListInterfaceInfoByPageQuery(
    //    int id,
    //    /**
    //    * 接口名称
    //    */
    //    string name,

    //    /**
    //     * 接口描述
    //     */
    //    string description,

    //    /**
    //     * 接口地址
    //     */
    //    string url,
    //    /**
    //     * 请求头
    //     */
    //    string requestHeader,

    //    /**
    //     * 响应头
    //     */
    //    string responseHeader,

    //    /**
    //     * 接口状态（0-关闭，1-开启）
    //     */
    //    int status,

    //    /**
    //     * 请求类型
    //     */
    //    string method,
    //    int userId,
    //    int current,
    //    int pageSize,
    //    string sortField,
    //    string sortOrder
    //    ) : IRequest<PaginatedList<InterfaceInfoSafetyResult>>;
}
