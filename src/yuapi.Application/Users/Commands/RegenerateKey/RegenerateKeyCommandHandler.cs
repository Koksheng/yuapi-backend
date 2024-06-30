using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Services.Common;
using yuapi.Application.Users.Common;
using yuapi.Domain.UserAggregate;

namespace yuapi.Application.Users.Commands.RegenerateKey
{
    public class RegenerateKeyCommandHandler :
        IRequestHandler<RegenerateKeyCommand, UserSafetyResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public RegenerateKeyCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<UserSafetyResult> Handle(RegenerateKeyCommand command, CancellationToken cancellationToken)
        {
            string userState = command.userState;

            // 1. Verify User using userId in userState
            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            // 2. get the to be deleted item using id
            User user = await _userRepository.GetUser(safetyUser.Id);
            if (user == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "User not found.");
            }

            string hashedAccessKey = EncryptionService.EncryptAccessKey(user.userAccount);
            string hashedSecretKey = EncryptionService.EncryptSecretKey(user.userAccount);

            user.accessKey = hashedAccessKey;
            user.secretKey = hashedSecretKey;
            user.updateTime = DateTime.Now;

            var result = await _userRepository.Update(user);

            if (result == 1)
            {
                UserSafetyResult safetyUserResult = _mapper.Map<UserSafetyResult>(user);
                return safetyUserResult;
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
