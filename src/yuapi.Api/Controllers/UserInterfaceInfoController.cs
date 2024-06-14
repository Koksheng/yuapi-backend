using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo;
using yuapi.Application.UserInterfaceInfos.Queries.GetUserInterfaceInfoById;
using yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfoByPage;
using yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfos;
using yuapi.Contracts.Common;
using yuapi.Contracts.UserInterfaceInfo;
using yuapi.Domain.Constants;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserInterfaceInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public UserInterfaceInfoController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addUserInterfaceInfo(CreateUserInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<CreateUserInterfaceInfoCommand>(request);
            // Assign the userId
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<BaseResponse<int>> deleteUserInterfaceInfo(DeleteRequest request)
        {
            if (request == null || request.id <= 0)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<DeleteUserInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<BaseResponse<int>> updateUserInterfaceInfo(UpdateUserInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<UpdateUserInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<BaseResponse<UserInterfaceInfoSafetyResponse>> getUserInterfaceInfoById(int id)
        {
            var query = new GetUserInterfaceInfoByIdQuery(id);

            var result = await _mediator.Send(query);

            // map result to response
            var response = _mapper.Map<UserInterfaceInfoSafetyResponse>(result);

            return ResultUtils.success(response);
        }

        //[AuthCheck("admin")]
        [HttpGet]
        public async Task<BaseResponse<List<UserInterfaceInfoSafetyResponse>>> listUserInterfaceInfo([FromQuery] QueryUserInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var query = _mapper.Map<ListUserInterfaceInfosQuery>(request);
            var result = await _mediator.Send(query);

            // map result to response
            var response = _mapper.Map<List<UserInterfaceInfoSafetyResponse>>(result);

            return ResultUtils.success(response);
        }

        [HttpGet("list/page")]
        public async Task<BaseResponse<PaginatedList<UserInterfaceInfoSafetyResponse>>> listUserInterfaceInfoByPage([FromQuery] QueryUserInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var query = _mapper.Map<ListUserInterfaceInfoByPageQuery>(request);
            var result = await _mediator.Send(query);

            var response = _mapper.Map<PaginatedList<UserInterfaceInfoSafetyResponse>>(result);

            return ResultUtils.success(response);
        }

    }
}
