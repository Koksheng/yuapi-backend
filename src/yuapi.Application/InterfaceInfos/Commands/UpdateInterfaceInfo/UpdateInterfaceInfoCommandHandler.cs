using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo
{
    public class UpdateInterfaceInfoCommandHandler :
        IRequestHandler<UpdateInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateInterfaceInfoCommandHandler(IInterfaceInfoRepository interfaceInfoRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(UpdateInterfaceInfoCommand command, CancellationToken cancellationToken)
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

            // 4. Map the updated data to the existing entity
            _mapper.Map(command, oldInterfaceInfo);
            oldInterfaceInfo.updateTime = DateTime.Now;

            // 5. Persist the updated entity
            var result = await _interfaceInfoRepository.Update(oldInterfaceInfo);

            if (result == 1)
            {
                //return ResultUtils.success(data: interfaceInfo.Id);
                return ResultUtils.success(data: oldInterfaceInfo.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
