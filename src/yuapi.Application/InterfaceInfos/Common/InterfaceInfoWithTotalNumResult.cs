namespace yuapi.Application.InterfaceInfos.Common
{
    public record InterfaceInfoWithTotalNumResult(
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
        int TotalNum
    );
}
