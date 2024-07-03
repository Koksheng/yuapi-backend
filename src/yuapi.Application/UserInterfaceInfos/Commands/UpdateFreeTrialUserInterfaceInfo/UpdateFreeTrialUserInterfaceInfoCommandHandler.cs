using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateFreeTrialUserInterfaceInfo
{
    public class UpdateFreeTrialUserInterfaceInfoCommandHandler :
        IRequestHandler<UpdateFreeTrialUserInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateFreeTrialUserInterfaceInfoCommandHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(UpdateFreeTrialUserInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            string userState = command.userState;
            int userId = command.userId;   
            int interfaceInfoId = command.interfaceInfoId;
            int lockNum = command.lockNum;
            int result;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. get the item 
            UserInterfaceInfo userInterfaceInfo = await _userInterfaceInfoRepository.GetByInterfaceInfoAndUserId(interfaceInfoId, userId);
            
            // 3. If is null, create
            if (userInterfaceInfo == null)
            {
                userInterfaceInfo = _mapper.Map<UserInterfaceInfo>(command);
                userInterfaceInfo.totalNum = 0;
                userInterfaceInfo.leftNum = lockNum;
                userInterfaceInfo.status = 1;
                userInterfaceInfo.createTime = DateTime.Now;
                userInterfaceInfo.updateTime = DateTime.Now;

                result = await _userInterfaceInfoRepository.Add(userInterfaceInfo);
            }
            // 4. Else, update
            else
            {
                // 3. Only the same userId is allowed to delete
                if (!userInterfaceInfo.userId.Equals(safetyUser.Id))
                {
                    throw new BusinessException(ErrorCode.NO_AUTH_ERROR, "User does not have permission to update this user interface info.");
                }

                // 4. If leftNum > 1000, return error
                if (userInterfaceInfo.leftNum > 1000)
                {
                    throw new BusinessException(ErrorCode.EXISTED_ERROR, "You have obtained too many Free Trial.");
                }

                // 4. Update userInterfaceInfo
                userInterfaceInfo.leftNum = lockNum + userInterfaceInfo.leftNum;
                userInterfaceInfo.updateTime = DateTime.Now;

                // 5. Persist the updated entity
                result = await _userInterfaceInfoRepository.Update(userInterfaceInfo);
            }
            

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
