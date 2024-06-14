namespace yuapi.Contracts.UserInterfaceInfo
{
    public record QueryUserInterfaceInfoRequest(
        int? id,
        int? userId,
        int? interfaceInfoId,
        int? totalNum,
        int? leftNum,
        int? status,
        int? current,
        int? pageSize,
        string? sortField,
        string? sortOrder
        );
}
