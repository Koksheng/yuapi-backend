namespace yuapi.Application.UserInterfaceInfos.Common
{
    public record UserInterfaceInfoSafetyResult(
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
