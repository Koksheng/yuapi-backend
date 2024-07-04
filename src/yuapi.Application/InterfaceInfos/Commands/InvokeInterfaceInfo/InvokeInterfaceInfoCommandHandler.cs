using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi_client_sdkyuapi_client_sdk.Client;
using yuapi.Domain.Enums;
using Newtonsoft.Json;
using yuapi_client_sdk.Model;
using yuapi.Application.Common.Utils;

namespace yuapi.Application.InterfaceInfos.Commands.InvokeInterfaceInfo
{
    public class InvokeInterfaceInfoCommandHandler :
        IRequestHandler<InvokeInterfaceInfoCommand, BaseResponse<object>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly YuApiClient _yuApiClient;

        public InvokeInterfaceInfoCommandHandler(IInterfaceInfoRepository interfaceInfoRepository, ICurrentUserService currentUserService, YuApiClient yuApiClient)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _currentUserService = currentUserService;
            _yuApiClient = yuApiClient;
        }

        public async Task<BaseResponse<object>> Handle(InvokeInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            int id = command.id;
            string userState = command.userState;
            string userRequestParams = command.userRequestParams;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. Get the InterfaceInfo using id
            InterfaceInfo interfaceInfo = await _interfaceInfoRepository.GetById(id);
            if (interfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "Interface info not found.");
            }
            if (interfaceInfo.status == (int)InterfaceInfoStatus.Offline)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "Interface info status is offline.");
            }

            _yuApiClient.SetAccessKey(safetyUser.accessKey);
            _yuApiClient.SetSecretKey(safetyUser.secretKey);

            try
            {
                var result = await _yuApiClient.InvokeAsync(interfaceInfo.name, userRequestParams);

                if (result is byte[] byteArrayResult)
                {
                    return ResultUtils.success<object>(Convert.ToBase64String(byteArrayResult));
                }

                return ResultUtils.success<object>(result);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return ResultUtils.error<object>(ErrorCode.SYSTEM_ERROR, ex.Message);
            }
        }
    }
}
