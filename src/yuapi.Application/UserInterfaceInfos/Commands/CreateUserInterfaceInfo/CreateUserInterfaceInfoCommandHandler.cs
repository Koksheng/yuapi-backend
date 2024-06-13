using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo
{
    public class CreateUserInterfaceInfoCommandHandler :
        IRequestHandler<CreateUserInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateUserInterfaceInfoCommandHandler(ICurrentUserService currentUserService, IMapper mapper, IUserInterfaceInfoRepository userInterfaceInfoRepository)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
        }

        public async Task<BaseResponse<int>> Handle(CreateUserInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            string userState = command.userState;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. Map Command to UserInterfaceInfo
            UserInterfaceInfo userInterfaceInfo = _mapper.Map<UserInterfaceInfo>(command);
            userInterfaceInfo.userId = safetyUser.Id;
            userInterfaceInfo.status = 1;
            userInterfaceInfo.createTime = DateTime.Now;
            userInterfaceInfo.updateTime = DateTime.Now;

            // 3. Persist UserInterfaceInfo
            var result = await _userInterfaceInfoRepository.Add(userInterfaceInfo);

            // 4. Return UserInterfaceInfo ID
            if (result == 1)
            {
                return ResultUtils.success(data: userInterfaceInfo.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
