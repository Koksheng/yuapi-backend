using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.Menus.Commands.CreateMenu;
using yuapi.Contracts.Menus;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public MenusController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> CreateMenu(CreateMenuRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var command = _mapper.Map<CreateMenuCommand>(request);
            // Assign the userId
            command = command with { UserId = "123456" };
            return await _mediator.Send(command);
        }
    }
}
