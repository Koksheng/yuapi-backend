using Grpc.Net.Client;
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
            try
            {
                await _client.UpdateCountAsync(request);
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
            }
            
        }
    }
}
