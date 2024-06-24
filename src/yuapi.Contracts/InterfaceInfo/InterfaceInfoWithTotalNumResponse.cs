namespace yuapi.Contracts.InterfaceInfo
{
    public record InterfaceInfoWithTotalNumResponse
    (
        int Id,
        string Name,
        string Description,
        string Url,
        string RequestParams,
        string RequestHeader,
        string ResponseHeader,
        int Status,
        string Method,
        DateTime CreateTime,
        DateTime UpdateTime,
        int TotalNum);
}
