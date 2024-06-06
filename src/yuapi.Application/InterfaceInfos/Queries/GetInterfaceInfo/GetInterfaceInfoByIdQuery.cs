using MediatR;
using yuapi.Application.InterfaceInfos.Common;

namespace yuapi.Application.InterfaceInfos.Queries.GetInterfaceInfo
{
    public class GetInterfaceInfoByIdQuery : IRequest<InterfaceInfoSafetyResult>
    {
        public int Id { get; }

        public GetInterfaceInfoByIdQuery(int id)
        {
            Id = id;
        }
    }
}
