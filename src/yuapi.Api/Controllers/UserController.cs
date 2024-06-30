using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Application.Users.Commands.RegenerateKey;
using yuapi.Application.Users.Commands.Register;
using yuapi.Application.Users.Commands.UpdateUser;
using yuapi.Application.Users.Commands.UpdateUserAvatar;
using yuapi.Application.Users.Queries.GetCurrentUser;
using yuapi.Application.Users.Queries.GetKey;
using yuapi.Application.Users.Queries.ListUserByPage;
using yuapi.Application.Users.Queries.Login;
using yuapi.Contracts.User;
using yuapi.Domain.Constants;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;
        //private const string USER_LOGIN_STATE = "userLoginState";
        private readonly IMapper _mapper;

        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<int>> userRegister(UserRegisterRequest request)
        {
            //var command = new UserRegisterCommand(request.userAccount, request.userPassword, request.checkPassword);
            var command = _mapper.Map<UserRegisterCommand>(request);
            return await _mediator.Send(command);
            //return await _userService.UserRegister(request);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<UserSafetyResponse>?> userLogin(UserLoginRequest request)
        {
            //var query = new UserLoginQuery(request.userAccount, request.userPassword);
            var query = _mapper.Map<UserLoginQuery>(request);
            var safetyUser = await _mediator.Send(query);

            // Convert user object to JSON string
            var serializedSafetyUser = JsonConvert.SerializeObject(safetyUser);

            // add user into session
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE)))
            {
                HttpContext.Session.SetString(ApplicationConstants.USER_LOGIN_STATE, serializedSafetyUser);
            }

            // map UserSafetyResult to UserSafetyResponse
            var response = _mapper.Map<UserSafetyResponse>(safetyUser);
            return ResultUtils.success(response);
        }

        [HttpPost]
        //[Authorize]
        public async Task<BaseResponse<int>> userLogout()
        {
            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);
            if (string.IsNullOrWhiteSpace(userState))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "session里找不到用户状态");
            }
            HttpContext.Session.Remove(ApplicationConstants.USER_LOGIN_STATE);
            //return 1;
            return ResultUtils.success(1);
        }

        [HttpGet]
        //[Authorize]
        public async Task<BaseResponse<UserSafetyResponse>?> getCurrentUser()
        {
            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var query = new GetCurrentUserQuery(userState);
            var currentSafetyUser = await _mediator.Send(query);

            // map UserSafetyResult to UserSafetyResponse
            var response = _mapper.Map<UserSafetyResponse>(currentSafetyUser);

            return ResultUtils.success(response);
        }

        //[HttpGet]
        //public async Task<BaseResponse<List<User>>?> searchUserList([FromQuery] SearchUserRequest request)
        //{

        //    User searchUser = _mapper.Map<User>(request);
        //    var userRquest = new User()
        //    {
        //        userName = request.username == null ? "" : request.username,
        //        userAccount = request.userAccount == null ? "" : request.userAccount,
        //        avatarUrl = request.avatarUrl == null ? "" : request.avatarUrl,
        //        gender = request.gender,
        //        phone = request.phone == null ? "" : request.phone,
        //        email = request.email == null ? "" : request.email,
        //        userStatus = request.userStatus,
        //        planetCode = request.planetCode == null ? "" : request.planetCode,
        //        userRole = request.userRole,
        //        createTime = request.createTime,
        //    };
        //    var safetyUsersList = await _userService.SearchUserList(userRquest);
        //    return ResultUtils.success(safetyUsersList);
        //}

        [HttpPost]
        public async Task<BaseResponse<int>> updateUser(UpdateUserRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = _mapper.Map<UpdateUserCommand>(request);
            // Assign the userState
            command = command with { userState = userState };
            return await _mediator.Send(command);
        }

        [HttpGet("list/page")]
        public async Task<BaseResponse<PaginatedList<AdminPageUserSafetyResponse>>> listUserByPage([FromQuery] QueryUserRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var query = _mapper.Map<ListUserByPageQuery>(request);
            var result = await _mediator.Send(query);

            var response = _mapper.Map<PaginatedList<AdminPageUserSafetyResponse>>(result);

            return ResultUtils.success(response);
        }

        [HttpPost("update/avatar")]
        public async Task<BaseResponse<int>> updateUserAvatar([FromForm] IFormFile file)
        {
            if (file == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var command = new UpdateUserAvatarCommand(file, userState);
            
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<BaseResponse<UserDevKeyResponse>> getKey()
        {
            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var query = new GetKeyQuery(userState);
            var currentSafetyUser = await _mediator.Send(query);

            // map UserSafetyResult to UserDevKeyResponse
            var response = _mapper.Map<UserDevKeyResponse>(currentSafetyUser);

            return ResultUtils.success(response);
        }

        [HttpPost]
        public async Task<BaseResponse<UserDevKeyResponse>> regenerateKey()
        {
            var userState = HttpContext.Session.GetString(ApplicationConstants.USER_LOGIN_STATE);

            var query = new RegenerateKeyCommand(userState);
            var currentSafetyUser = await _mediator.Send(query);

            // map UserSafetyResult to UserDevKeyResponse
            var response = _mapper.Map<UserDevKeyResponse>(currentSafetyUser);

            return ResultUtils.success(response);
        }
    }
}
