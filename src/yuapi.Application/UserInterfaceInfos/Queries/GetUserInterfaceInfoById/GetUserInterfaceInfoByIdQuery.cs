using MediatR;
using yuapi.Application.UserInterfaceInfos.Common;

namespace yuapi.Application.UserInterfaceInfos.Queries.GetUserInterfaceInfoById
{
    public class GetUserInterfaceInfoByIdQuery : IRequest<UserInterfaceInfoSafetyResult>
    {
        public int Id { get; }

        public GetUserInterfaceInfoByIdQuery(int id)
        {
            Id = id;
        }
    }
}
