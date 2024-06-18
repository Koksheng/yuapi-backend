using Grpc.Core;
using yuapi.Application.Common.Interfaces.Persistence;

namespace yuapi.RPC.InvokeCountService.Services
{
    public class InvokeCountService : InvokeCount.InvokeCountBase
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;

        public InvokeCountService(IUserInterfaceInfoRepository userInterfaceInfoRepository)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
        }

        public override async Task<UpdateCountReply> UpdateCount(UpdateCountRequest request, ServerCallContext context)
        {
            var userInterfaceInfo = await _userInterfaceInfoRepository.GetByInterfaceInfoAndUserId(request.InterfaceInfoId, request.UserId);
            if (userInterfaceInfo == null)
            {
                return new UpdateCountReply { Success = false };
            }

            userInterfaceInfo.leftNum -= 1;
            userInterfaceInfo.totalNum += 1;

            await _userInterfaceInfoRepository.Update(userInterfaceInfo);

            return new UpdateCountReply { Success = true };
        }
    }
}
