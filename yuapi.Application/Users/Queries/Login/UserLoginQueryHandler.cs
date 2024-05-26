using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Authentication;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Services.Common;
using yuapi.Application.Users.Commands.Register;
using yuapi.Contracts.User;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace yuapi.Application.Users.Queries.Login
{
    public class UserLoginQueryHandler :
        IRequestHandler<UserLoginQuery, UserSafetyResponse?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserLoginQueryHandler(IUserRepository userRepository, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserSafetyResponse?> Handle(UserLoginQuery query, CancellationToken cancellationToken)
        {
            string userAccount = query.userAccount;
            string userPassword = query.userPassword;

            // 2. check user is exist
            if (await _userRepository.GetUserByUserAccount(userAccount) is not User user)
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
            UserSafetyResponse safetyUser = _mapper.Map<UserSafetyResponse>(user);

            // 4. JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.userName);
            // Assign the generated token
            safetyUser = safetyUser with { token = token };

            return safetyUser;
        }
    }
}
