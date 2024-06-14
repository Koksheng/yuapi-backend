namespace yuapi.Contracts.UserInterfaceInfo
{
    public record UserInterfaceInfoSafetyResponse(
        int id,
        int userId,
        int interfaceInfoId,
        int totalNum,
        int leftNum,
        int status,
        DateTime createTime,
        DateTime updateTime,
        int isDelete
        );
}
