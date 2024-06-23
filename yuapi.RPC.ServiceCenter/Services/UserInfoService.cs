using Grpc.Core;
using yuapi.Application.Common.Interfaces.Persistence;

namespace yuapi.RPC.ServiceCenter.Services
{
    public class UserInfoService : UserInfo.UserInfoBase
    {
        private readonly IUserRepository _userRepository;

        public UserInfoService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<UserInfoResponse> GetUserInfo(UserInfoRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetUserInfoByAccessKey(request.AccessKey);
            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            var response = new UserInfoResponse
            {
                Id = user.Id.Value,
                UserName = user.userName,
                UserAccount= user.userAccount,
                SecretKey = user.secretKey,
            };

            return await Task.FromResult(response);
        }
    }
}
