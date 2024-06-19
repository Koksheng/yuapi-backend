using Grpc.Net.Client;
using yuapi.RPC.InvokeCountService;
namespace yuapi_OcelotGateway.Services
{
    public class InvokeCountServiceClient
    {
        private readonly InvokeCount.InvokeCountClient _client;

        public InvokeCountServiceClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new InvokeCount.InvokeCountClient(channel);
        }

        public async Task UpdateCountAsync(int interfaceInfoId, int userId)
        {
            var request = new UpdateCountRequest
            {
                InterfaceInfoId = interfaceInfoId,
                UserId = userId
            };
            await _client.UpdateCountAsync(request);
        }
    }
}
