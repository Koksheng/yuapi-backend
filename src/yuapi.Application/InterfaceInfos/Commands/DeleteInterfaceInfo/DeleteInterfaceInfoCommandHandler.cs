using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo
{
    public class DeleteInterfaceInfoCommandHandler : 
        IRequestHandler<DeleteInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        public DeleteInterfaceInfoCommandHandler(ICurrentUserService currentUserService, IInterfaceInfoRepository interfaceInfoRepository)
        {
            _currentUserService = currentUserService;
            _interfaceInfoRepository = interfaceInfoRepository;
        }

        public async Task<BaseResponse<int>> Handle(DeleteInterfaceInfoCommand command, CancellationToken cancellationToken)
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

            int result = await _interfaceInfoRepository.DeleteById(id);

            if (result == 0)
                throw new BusinessException(ErrorCode.STSTEM_ERROR, "删除失败，数据库错误");


            return ResultUtils.success(data: id);
        }
    }
}
