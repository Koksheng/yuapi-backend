using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.MenuAggregate;

namespace yuapi.Application.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler :
        IRequestHandler<CreateMenuCommand, BaseResponse<int>>
    {
        private readonly IMenuRepository _menuRepository;

        public CreateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<BaseResponse<int>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Create Menu
            var menu = Menu.Create(
                name: request.Name,
                description: request.Description,
                url: request.Url,
                requestHeader: request.RequestHeader,
                responseHeader: request.ResponseHeader,
                userId: request.UserId,
                status: request.Status,
                method: request.Method
                );

            // Persist InterfaceInfo
            var result = await _menuRepository.Add(menu);

            // Return InterfaceInfo or 1
            if (result == 1)
            {
                //return ResultUtils.success(data: interfaceInfo.Id);
                return ResultUtils.success(data: result);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
