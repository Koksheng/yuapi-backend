namespace yuapi.Contracts.UserInterfaceInfo
{
    public record UpdateFreeTrialUserInterfaceInfoRequest(
        int userId,
        int interfaceInfoId,
        int lockNum
        );
}
