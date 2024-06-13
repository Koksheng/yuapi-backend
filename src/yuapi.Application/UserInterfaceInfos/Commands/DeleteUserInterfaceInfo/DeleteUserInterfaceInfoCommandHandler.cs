using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo
{
    public class DeleteUserInterfaceInfoCommandHandler :
        IRequestHandler<DeleteUserInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;

        public DeleteUserInterfaceInfoCommandHandler(ICurrentUserService currentUserService, IUserInterfaceInfoRepository userInterfaceInfoRepository)
        {
            _currentUserService = currentUserService;
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
        }

        public async Task<BaseResponse<int>> Handle(DeleteUserInterfaceInfoCommand command, CancellationToken cancellationToken)
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
                    throw new BusinessException(ErrorCode.NO_AUTH_ERROR, "User does not have permission to delete this user interface info.");
                }
            }

            int result = await _userInterfaceInfoRepository.DeleteById(id);

            if (result == 0)
                throw new BusinessException(ErrorCode.SYSTEM_ERROR, "删除失败，数据库错误");


            return ResultUtils.success(data: id);
        }
    }
}
