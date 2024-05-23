using AutoMapper;
using Azure.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Authentication;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Migrations;
using yuapi.Application.Services.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Contracts.User;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;

namespace yuapi.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<BaseResponse<int>> UserRegister(UserRegisterRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }
            string userAccount = request.userAccount;
            string userPassword = request.userPassword;
            string checkPassword = request.checkPassword;
            // 1. Verify
            if (string.IsNullOrWhiteSpace(userAccount) || string.IsNullOrWhiteSpace(userPassword) || string.IsNullOrWhiteSpace(checkPassword))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "请求参数为空");
            }
            if (userAccount.Length < 4)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "用户账户过短");
            }
            if (userPassword.Length < 8 || checkPassword.Length < 8)
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "账户密码过短");

            // userPassword & checkPassword must same
            if (!userPassword.Equals(checkPassword))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "账户密码与检查密码不对等");
            }

            // userAccount cant existed
            var user = await _userRepository.GetUserByUserAccount(userAccount);
            if (user != null)
            {
                if (user.isDelete == false)
                    throw new BusinessException(ErrorCode.EXISTED_ERROR, "用户账户已有注册记录");
            }

            // 2. 加密 (.net core IdentityUser will encrypt themself
            string hashedPassword = EncryptionService.EncryptPassword(userPassword);

            // 3. Insert User to DB

            User newUser = _mapper.Map<User>(request);
            newUser.userPassword = hashedPassword;
            newUser.userName = "test";
            newUser.accessKey = "access";
            newUser.secretKey = "secret";
            newUser.userAvatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRpy6bicoFta2pSa5I3U1mKbUQPEB7Hxobc0oVEKp2YZknVoJlq0CjgtrbxEFSM4O6F8Dg&usqp=CAU";
            newUser.createTime = DateTime.Now;

            int result = await _userRepository.CreateUser(newUser);

            if (result == 0)
                throw new BusinessException(ErrorCode.STSTEM_ERROR, "注册失败，数据库错误");

            return ResultUtils.success(newUser.Id);
        }

        public async Task<UserSafetyResponse?> UserLogin(string userAccount, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userAccount) || string.IsNullOrWhiteSpace(userPassword))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "请求参数为空");
            }
            if (userAccount.Length < 4)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "用户账户过短");
            }
            if (userPassword.Length < 8)
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "账户密码过短");

            // userAccount cant contain special character
            string pattern = @"[^a-zA-Z0-9\s]";
            if (Regex.IsMatch(userAccount, pattern))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "用户账户有特殊字符");
            }

            // 2. check user is exist
            var user = await _userRepository.GetUserByUserAccount(userAccount);
            if (user == null)
            {
                throw new BusinessException(ErrorCode.NULL_ERROR, "找不到该用户");
            }
            if (user.isDelete == true)
            {
                throw new BusinessException(ErrorCode.NULL_ERROR, "找不到该用户 用户已被删除");
            }
            if (!EncryptionService.VerifyPassword(user.userPassword, userPassword))
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR, "账户密码不对");
            }

            // 3. 用户脱敏 desensitization
            UserSafetyResponse safetyUser = await GetSafetyUser(user);

            // 4. JWT Token
            int newUserId = user.Id;
            string userName = user.userName;
            var token = _jwtTokenGenerator.GenerateToken(newUserId, userName);
            // Assign the generated token
            safetyUser = safetyUser with { token = token };

            return safetyUser;
        }

        public async Task<UserSafetyResponse> GetSafetyUser(User user)
        {
            var safetyUser = _mapper.Map<UserSafetyResponse>(user);

            //User safetyUser = new User(
            //    user.Id,
            //    user.userName,
            //    user.userAccount,
            //    user.avatarUrl,
            //    user.gender,
            //    user.phone,
            //    user.email,
            //    user.userStatus,
            //    user.createTime,
            //    user.updateTime,
            //    user.isDelete,
            //    user.userRole,
            //    user.planetCode
            //    );

            //// Access the newly generated user ID
            //int newUserId = user.Id;
            //string userName = user.userName;
            //var token = _jwtTokenGenerator.GenerateToken(newUserId, userName);

            return safetyUser;
        }

        public async Task<UserSafetyResponse> GetCurrentUser(string userState)
        {
            // 1. get user by id
            var loggedInUser = JsonConvert.DeserializeObject<User>(userState);
            var user = await _userRepository.GetUser(loggedInUser.Id);

            if (user == null || user.isDelete)
            {
                //return null;
                throw new BusinessException(ErrorCode.NULL_ERROR, "找不到该用户");
            }
            // 3. 用户脱敏 desensitization
            UserSafetyResponse safetyUser = await GetSafetyUser(user);
            //var safetyUser = await _userRepository.GetSafetyUser(user);
            //safetyUser.IsAdmin = await verifyIsAdminRoleAsync();
            //return safetyUser;
            return safetyUser;
        }

        public async Task<BaseResponse<List<UserSafetyResponse>>?> SearchUserList(SearchUserRequest request)
        {
            User userRquest = _mapper.Map<User>(request);
            //var userRquest = new User()
            //{
            //    userName = request.username == null ? "" : request.username,
            //    userAccount = request.userAccount == null ? "" : request.userAccount,
            //    avatarUrl = request.avatarUrl == null ? "" : request.avatarUrl,
            //    gender = request.gender,
            //    phone = request.phone == null ? "" : request.phone,
            //    email = request.email == null ? "" : request.email,
            //    userStatus = request.userStatus,
            //    planetCode = request.planetCode == null ? "" : request.planetCode,
            //    userRole = request.userRole,
            //    createTime = request.createTime,
            //};
            //var safetyUsersList = await _userService.SearchUserList(userRquest);
            //var query = _context.Users.Where(u => !u.isDelete);

            //if (!string.IsNullOrEmpty(user.userName))
            //{
            //    query = query.Where(u => u.userName.Contains(user.userName));
            //}

            //if (!string.IsNullOrEmpty(user.userAccount))
            //{
            //    query = query.Where(u => u.userAccount.Contains(user.userAccount));
            //}

            //if (!string.IsNullOrEmpty(user.avatarUrl))
            //{
            //    query = query.Where(u => u.avatarUrl.Contains(user.avatarUrl));
            //}

            //if (user.gender != 0)
            //{
            //    query = query.Where(u => u.gender == user.gender);
            //}

            //if (!string.IsNullOrEmpty(user.phone))
            //{
            //    query = query.Where(u => u.phone.Contains(user.phone));
            //}

            //if (!string.IsNullOrEmpty(user.email))
            //{
            //    query = query.Where(u => u.email.Contains(user.email));
            //}

            //if (user.userStatus != 0)
            //{
            //    query = query.Where(u => u.userStatus == user.userStatus);
            //}

            //if (!string.IsNullOrEmpty(user.planetCode))
            //{
            //    query = query.Where(u => u.planetCode.Contains(user.planetCode));
            //}

            //if (user.userRole != 0)
            //{
            //    query = query.Where(u => u.userRole == user.userRole);
            //}

            //Check if createTime has been set(not default)
            //if (user.createTime != default(DateTime))
            //{
            //    query = query.Where(u => u.createTime == user.createTime);
            //}

            //var users = await _userRepository.SearchUserByFilter(query);

            //Create a list to store simplified user objects
            List<UserSafetyResponse> safetyUsersList = new List<UserSafetyResponse>();

            //Loop through each user and call getSafetyUser to get simplified user object
            //foreach (var u in users)
            //{
            //    var safetyUser = await _userRepository.GetSafetyUser(u);
            //    safetyUsersList.Add(safetyUser);

            //}


            return ResultUtils.success(safetyUsersList);
        }
    }
}
