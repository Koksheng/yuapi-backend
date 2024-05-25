using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Services.Common;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;

namespace yuapi.Application.Users.Commands.Register
{
    public class UserRegisterCommandHandler :
        IRequestHandler<UserRegisterCommand, BaseResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserRegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }
            string userAccount = command.userAccount;
            string userPassword = command.userPassword;
            string checkPassword = command.checkPassword;
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

            User newUser = _mapper.Map<User>(command);
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
    }
}
