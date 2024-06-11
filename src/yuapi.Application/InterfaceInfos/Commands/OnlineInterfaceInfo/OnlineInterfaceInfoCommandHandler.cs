using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi_client_sdkyuapi_client_sdk.Client;

namespace yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo
{
    public class OnlineInterfaceInfoCommandHandler :
        IRequestHandler<OnlineInterfaceInfoCommand, BaseResponse<bool>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly YuApiClient _yuApiClient;
        public OnlineInterfaceInfoCommandHandler(ICurrentUserService currentUserService, IInterfaceInfoRepository interfaceInfoRepository, YuApiClient yuApiClient)
        {
            _currentUserService = currentUserService;
            _interfaceInfoRepository = interfaceInfoRepository;
            _yuApiClient = yuApiClient;
        }

        public async Task<BaseResponse<bool>> Handle(OnlineInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            int id = command.id;
            string userState = command.userState;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. get the to be deleted item using id
            InterfaceInfo oldInterfaceInfo = await _interfaceInfoRepository.GetById(id);
            if (oldInterfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "Interface info not found.");
            }

            // 3. Only the same userId or admin is allowed to delete
            if (!oldInterfaceInfo.userId.Equals(safetyUser.Id))
            {
                var isAdmin = await _currentUserService.IsAdminAsync(safetyUser);
                if (!isAdmin)
                {
                    throw new BusinessException(ErrorCode.NO_AUTH_ERROR, "User does not have permission to delete this interface info.");
                }
            }

            // 4. 判断该接口是否可以调用 Verify if the interface can be invoked using YuApiClient

            var yuapiClient_result = await _yuApiClient.GetNameByGet("interfaceCheck");
            if (yuapiClient_result == "")
            {
                throw new BusinessException(ErrorCode.SYSTEM_ERROR, "Interface invocation check failed.");
            }

            int result = await _interfaceInfoRepository.OnlineInterfaceInfoById(id);

            if (result == 0)
                throw new BusinessException(ErrorCode.SYSTEM_ERROR, "update失败，数据库错误");


            return ResultUtils.success(data: true);
        }
    }
}
