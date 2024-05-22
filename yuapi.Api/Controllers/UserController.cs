using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using yuapi.Application.Services.Users;
using yuapi.Contracts.User;
using yuapi.Domain.Common;

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
        public async Task<BaseResponse<int>> userRegister(UserRegisterRequest request)
        {

            return await _userService.UserRegister(request);
        }

        [HttpPost]
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

    }
}
