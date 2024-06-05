using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Constants;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterfaceInfoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public InterfaceInfoController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addInterfaceInfo(CreateInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<CreateInterfaceInfoCommand>(request);
            // Assign the userId
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<BaseResponse<int>> deleteInterfaceInfo(DeleteInterfaceInfoRequest request)
        {
            if (request == null || request.id <= 0)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var id = request.id;

            var command = _mapper.Map<DeleteInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<BaseResponse<int>> updateInterfaceInfo(UpdateInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var id = request.id;

            var command = _mapper.Map<DeleteInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }
    }
}
