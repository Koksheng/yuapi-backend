namespace yuapi_OcelotGateway.Services
{
    public interface IUserVerificationService
    {
        Task<bool> VerifyUserAsync(string accessKey, string secretKey);
    }
    public class UserVerificationService : IUserVerificationService
    {
        public Task<bool> VerifyUserAsync(string accessKey, string secretKey)
        {
            // Implement user verification logic
            return Task.FromResult(true);
        }
    }
}
