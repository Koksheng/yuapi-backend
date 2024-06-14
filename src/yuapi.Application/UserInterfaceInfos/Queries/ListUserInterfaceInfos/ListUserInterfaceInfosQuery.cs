using MediatR;
using yuapi.Application.UserInterfaceInfos.Common;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfos
{
    public record ListUserInterfaceInfosQuery(
        int id,
        int userId,
        int interfaceInfoId,
        int totalNum,
        int leftNum,
        int status
        ) : IRequest<List<UserInterfaceInfoSafetyResult>>;
}
