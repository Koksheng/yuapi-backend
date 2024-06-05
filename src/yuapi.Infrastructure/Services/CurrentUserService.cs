using AutoMapper;
using Newtonsoft.Json;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Users.Common;
using yuapi.Domain.Common;
using yuapi.Domain.Constants;
using yuapi.Domain.Exception;

namespace yuapi.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CurrentUserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserSafetyResult> GetCurrentUserAsync(string userState)
        {
            if (string.IsNullOrEmpty(userState))
            {
                throw new BusinessException(ErrorCode.NO_AUTH_ERROR);
            }
            // 1. get user by id
            var loggedInUser = JsonConvert.DeserializeObject<UserSafetyResult>(userState);
            var user = await _userRepository.GetUser(loggedInUser.Id);

            if (user == null || user.isDelete)
            {
                //return null;
                throw new BusinessException(ErrorCode.NULL_ERROR, "找不到该用户");
            }
            // 3. 用户脱敏 desensitization
            UserSafetyResult safetyUser = _mapper.Map<UserSafetyResult>(user);
            return safetyUser;
        }

        public async Task<bool> IsAdminAsync(UserSafetyResult safetyUser)
        {
            if (safetyUser == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }
            var isAdmin = ApplicationConstants.ADMIN_ROLE.Equals(safetyUser.userRole);
            return isAdmin;
        }
    }
}
