using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Api.AuthCheck;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.InvokeInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.OfflineInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Queries.GetInterfaceInfo;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos;
using yuapi.Contracts.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [HttpGet]
        public async Task<BaseResponse<InterfaceInfoSafetyResponse>> getInterfaceInfoById(int id)
        {
            var query = new GetInterfaceInfoByIdQuery(id);

            var result= await _mediator.Send(query);

            // map result to response
            var response = _mapper.Map<InterfaceInfoSafetyResponse>(result);

            return ResultUtils.success(response);
        }

        //[AuthCheck("admin")]
        [HttpGet]
        public async Task<BaseResponse<List<InterfaceInfoSafetyResponse>>> listInterfaceInfo([FromQuery] QueryInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var query = _mapper.Map<ListInterfaceInfosQuery>(request);
            var result = await _mediator.Send(query);

            // map result to response
            var response = _mapper.Map<List<InterfaceInfoSafetyResponse>>(result);

            return ResultUtils.success(response);
        }

        [HttpGet("list/page")]
        public async Task<BaseResponse<PaginatedList<InterfaceInfoSafetyResponse>>> listInterfaceInfoByPage([FromQuery] QueryInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var query = _mapper.Map<ListInterfaceInfoByPageQuery>(request);
            var result = await _mediator.Send(query);

            var response = _mapper.Map<PaginatedList<InterfaceInfoSafetyResponse>>(result);

            return ResultUtils.success(response);
        }

        //[AuthCheck("admin")]
        [HttpPost]
        public async Task<BaseResponse<bool>> onlineInterfaceInfo(IdRequest request)
        {
            if (request == null || request.id <= 0)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<OnlineInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        //[AuthCheck("admin")]
        [HttpPost]
        public async Task<BaseResponse<bool>> offlineInterfaceInfo(IdRequest request)
        {
            if (request == null || request.id <= 0)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<OfflineInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<BaseResponse<string>> invokeInterfaceInfo(InvokeInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);
            
            var command = _mapper.Map<InvokeInterfaceInfoCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }
    }
}
