using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo
{
    public class CreateInterfaceInfoCommandHandler :
        IRequestHandler<CreateInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateInterfaceInfoCommandHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<int>> Handle(CreateInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            string userState = command.userState;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);


            // 2. Map Command to InterfaceInfo
            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(command);
            interfaceInfo.userId = safetyUser.Id;
            interfaceInfo.createTime = DateTime.Now;
            interfaceInfo.updateTime = DateTime.Now;

            // 3. Persist InterfaceInfo
            var result =  await _interfaceInfoRepository.Add(interfaceInfo);

            // 4. Return InterfaceInfo ID
            if (result == 1)
            {
                //return ResultUtils.success(data: interfaceInfo.Id);
                return ResultUtils.success(data: interfaceInfo.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
