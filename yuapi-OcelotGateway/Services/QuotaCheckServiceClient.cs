using Grpc.Net.Client;
namespace yuapi_OcelotGateway.Services
{
    public class QuotaCheckServiceClient
    {
        private readonly QuotaCheck.QuotaCheckClient _client;
        public QuotaCheckServiceClient(string grpcAddress)
        {
            var channel = GrpcChannel.ForAddress(grpcAddress);
            _client = new QuotaCheck.QuotaCheckClient(channel);
        }

        public async Task<QuotaCheckReply> GetUserInterfaceInfoAsync(int interfaceInfoId, int userId)
        {
            var request = new QuotaCheckRequest
            {
                InterfaceInfoId = interfaceInfoId,
                UserId = userId
            };
            try
            {
                return await _client.CheckQuotaAsync(request);
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new QuotaCheckReply
                {
                    Success = false
                };
            }

        }
    }
}
