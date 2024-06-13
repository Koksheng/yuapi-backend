using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo
{
    public class UpdateUserInterfaceInfoCommandHandler :
        IRequestHandler<UpdateUserInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateUserInterfaceInfoCommandHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(UpdateUserInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            int id = command.id;
            string userState = command.userState;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. get the to be deleted item using id
            UserInterfaceInfo oldUserInterfaceInfo = await _userInterfaceInfoRepository.GetById(id);
            if (oldUserInterfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "User Interface Info not found.");
            }

            // 3. Only the same userId or admin is allowed to delete
            if (!oldUserInterfaceInfo.userId.Equals(safetyUser.Id))
            {
                var isAdmin = await _currentUserService.IsAdminAsync(safetyUser);
                if (!isAdmin)
                {
                    throw new BusinessException(ErrorCode.NO_AUTH_ERROR, "User does not have permission to update this user interface info.");
                }
            }

            // 4. Map the updated data to the existing entity
            _mapper.Map(command, oldUserInterfaceInfo);
            oldUserInterfaceInfo.updateTime = DateTime.Now;

            // 5. Persist the updated entity
            var result = await _userInterfaceInfoRepository.Update(oldUserInterfaceInfo);

            if (result == 1)
            {
                return ResultUtils.success(data: oldUserInterfaceInfo.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }

        }
    }
}
