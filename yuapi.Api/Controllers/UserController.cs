using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yuapi.Application.Services.Users;
using yuapi.Contracts.User;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private const string USER_LOGIN_STATE = "userLoginState";

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<int>> userRegister(UserRegisterRequest request)
        {

            return await _userService.UserRegister(request);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<BaseResponse<UserSafetyResponse>?> userLogin(UserLoginRequest request)
        {
            var safetyUser = await _userService.UserLogin(request.userAccount, request.userPassword);

            // Convert user object to JSON string
            var serializedSafetyUser = JsonConvert.SerializeObject(safetyUser);

            // add user into session
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(USER_LOGIN_STATE)))
            {
                HttpContext.Session.SetString(USER_LOGIN_STATE, serializedSafetyUser);
            }
            return ResultUtils.success(safetyUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<BaseResponse<int>> userLogout()
        {
            var userState = HttpContext.Session.GetString(USER_LOGIN_STATE);
            if (string.IsNullOrWhiteSpace(userState))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "session里找不到用户状态");
            }
            HttpContext.Session.Remove(USER_LOGIN_STATE);
            //return 1;
            return ResultUtils.success(1);
        }

        [HttpGet]
        [Authorize]
        public async Task<BaseResponse<UserSafetyResponse>?> getCurrentUser()
        {
            var userState = HttpContext.Session.GetString(USER_LOGIN_STATE);
            if (string.IsNullOrWhiteSpace(userState))
            {
                //return null;
                throw new BusinessException(ErrorCode.NOT_LOGIN);
            }

            var currentSafetyUser = await _userService.GetCurrentUser(userState);

            return ResultUtils.success(currentSafetyUser);
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
    }
}
