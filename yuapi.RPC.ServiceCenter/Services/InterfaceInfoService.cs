using Grpc.Core;
using yuapi.Application.Common.Interfaces.Persistence;

namespace yuapi.RPC.ServiceCenter.Services
{
    public class InterfaceInfoService : InterfaceInfo.InterfaceInfoBase
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository; 

        public InterfaceInfoService(IInterfaceInfoRepository interfaceInfoRepository)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
        }

        public override async Task<InterfaceInfoResponse> GetInterfaceInfo(InterfaceInfoRequest request, ServerCallContext context)
        {
            var interfaceInfo = await _interfaceInfoRepository.GetInterfaceInfo(request.Path, request.Method); // Your logic to get interface info

            if (interfaceInfo == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Interface not found"));
            }
            var response = new InterfaceInfoResponse
            {
                Id = interfaceInfo.Id.Value,
                Name = interfaceInfo.name
            };

            return await Task.FromResult(response);
        }
    }
}
