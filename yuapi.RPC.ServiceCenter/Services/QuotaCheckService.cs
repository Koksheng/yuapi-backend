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

            // Map domain model to proto message
            var userInterfaceInfoMessage = new UserInterfaceInfo
            {
                UserId = userInterfaceInfo.userId,
                InterfaceInfoId = userInterfaceInfo.interfaceInfoId,
                TotalNum = userInterfaceInfo.totalNum,
                LeftNum = userInterfaceInfo.leftNum,
                Status = userInterfaceInfo.status,
                CreateTime = userInterfaceInfo.createTime.ToString("o"), // ISO 8601 format,
                UpdateTime = userInterfaceInfo.updateTime.ToString("o")  // ISO 8601 format
            };

            return new QuotaCheckReply
            {
                Success = true,
                UserInterfaceInfo = userInterfaceInfoMessage
            };
        }
    }
}
