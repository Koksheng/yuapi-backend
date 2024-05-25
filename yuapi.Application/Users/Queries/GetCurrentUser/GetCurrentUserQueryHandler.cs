using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Users.Queries.Login;
using yuapi.Contracts.User;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;

namespace yuapi.Application.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler :
        IRequestHandler<GetCurrentUserQuery, UserSafetyResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserSafetyResponse> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }
            string userState = query.userState;
            // 1. get user by id
            var loggedInUser = JsonConvert.DeserializeObject<User>(userState);
            var user = await _userRepository.GetUser(loggedInUser.Id);

            if (user == null || user.isDelete)
            {
                //return null;
                throw new BusinessException(ErrorCode.NULL_ERROR, "找不到该用户");
            }
            // 3. 用户脱敏 desensitization
            UserSafetyResponse safetyUser = _mapper.Map<UserSafetyResponse>(user);
            //UserSafetyResponse safetyUser = await GetSafetyUser(user);
            //var safetyUser = await _userRepository.GetSafetyUser(user);
            //safetyUser.IsAdmin = await verifyIsAdminRoleAsync();
            //return safetyUser;
            return safetyUser;
        }
    }
}
