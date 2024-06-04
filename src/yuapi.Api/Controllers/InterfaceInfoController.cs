using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo;
using yuapi.Application.Services.Users;
using yuapi.Application.Users.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterfaceInfoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private const string USER_LOGIN_STATE = "userLoginState";
        private readonly IUserService _userService;

        public InterfaceInfoController(IMapper mapper, ISender mediator, IUserService userService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addInterfaceInfo(CreateInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(USER_LOGIN_STATE);
            var safetyUser = await _userService.GetCurrentUser(userState);

            var command = _mapper.Map<CreateInterfaceInfoCommand>(request);
            // Assign the userId
            command = command with { userId = safetyUser.Id.ToString() };
            return await _mediator.Send(command);
        }

        //[HttpPost]
        //public async Task<BaseResponse<int>> deleteInterfaceInfo(DeleteInterfaceInfoRequest request)
        //{
        //    if (request == null || request.id <= 0)
        //    {
        //        throw new BusinessException(ErrorCode.PARAMS_ERROR);
        //    }

        //    var userState = HttpContext.Session.GetString(USER_LOGIN_STATE);
        //    //var safetyUser = await _userService.GetCurrentUser(userState);


        //    var id = request.id;

        //    //

        //    var command = _mapper.Map<DeleteInterfaceInfoCommand>(request);
        //    // Assign the userState
        //    command = command with { userState = userState };
        //    return await _mediator.Send(command);
        //}
    }
}
