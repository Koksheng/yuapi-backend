using Grpc.Net.Client;

namespace yuapi_OcelotGateway.Services
{
    public class InterfaceInfoServiceClient
    {
        private readonly InterfaceInfo.InterfaceInfoClient _client;

        public InterfaceInfoServiceClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new InterfaceInfo.InterfaceInfoClient(channel);
        }

        public async Task<InterfaceInfoResponse> GetInterfaceInfoAsync(string path, string method)
        {
            var request = new InterfaceInfoRequest { Path = path, Method = method };
            return await _client.GetInterfaceInfoAsync(request);
        }
    }
}
