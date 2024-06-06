using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Api.AuthCheck;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Queries;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Contracts.User;
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

            var command = _mapper.Map<UpdateInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [AuthCheck("admin")]
        [HttpGet]
        public async Task<BaseResponse<InterfaceInfoSafetyResponse>> getInterfaceInfoById(int id)
        {
            var query = new GetInterfaceInfoByIdQuery(id);

            var result= await _mediator.Send(query);

            // map result to response
            var response = _mapper.Map<InterfaceInfoSafetyResponse>(result);

            return ResultUtils.success(response);
        }
    }
}
