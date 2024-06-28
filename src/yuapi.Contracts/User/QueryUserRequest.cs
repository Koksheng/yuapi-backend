namespace yuapi.Contracts.User
{
    public record QueryUserRequest(
        int? id,
        string? userName,
        string? userAccount,
        //string? userAvatar,
        int? gender,
        string? userRole,
        int? current,
        int? pageSize,
        string? sortField,
        string? sortOrder
        );
}
