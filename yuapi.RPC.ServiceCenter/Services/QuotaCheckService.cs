using Grpc.Core;
using yuapi.Application.Common.Interfaces.Persistence;

namespace yuapi.RPC.ServiceCenter.Services
{
    public class QuotaCheckService : QuotaCheck.QuotaCheckBase
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;

        public QuotaCheckService(IUserInterfaceInfoRepository userInterfaceInfoRepository)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
        }
        public override async Task<QuotaCheckReply> CheckQuota(QuotaCheckRequest request, ServerCallContext context)
        {
            var userInterfaceInfo = await _userInterfaceInfoRepository.GetByInterfaceInfoAndUserId(request.InterfaceInfoId, request.UserId);
            if (userInterfaceInfo == null)
            {
                return new QuotaCheckReply { Success = false };
            }

            userInterfaceInfo.leftNum -= 1;
            userInterfaceInfo.totalNum += 1;
            userInterfaceInfo.updateTime = DateTime.Now;

            await _userInterfaceInfoRepository.Update(userInterfaceInfo);

            return new QuotaCheckReply { Success = true };
        }
    }
}
