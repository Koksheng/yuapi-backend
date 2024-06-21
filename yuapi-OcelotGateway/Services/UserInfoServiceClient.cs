using Grpc.Net.Client;

namespace yuapi_OcelotGateway.Services
{
    public class UserInfoServiceClient
    {
        private readonly UserInfo.UserInfoClient _client;
        public UserInfoServiceClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new UserInfo.UserInfoClient(channel);
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(string accessKey)
        {
            var request = new UserInfoRequest { AccessKey = accessKey };
            return await _client.GetUserInfoAsync(request);
        }
    }
}
