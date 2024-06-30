using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Domain.UserAggregate;

namespace yuapi.Application.Users.Commands.UpdateUserAvatar
{
    public class UpdateUserAvatarCommandHandler :
        IRequestHandler<UpdateUserAvatarCommand, BaseResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public UpdateUserAvatarCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IMapper mapper, IFileUploadService fileUploadService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }

        public async Task<BaseResponse<int>> Handle(UpdateUserAvatarCommand command, CancellationToken cancellationToken)
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

            // 3. update to local project avatars folder
            var avatarUrl = await _fileUploadService.UploadFileAvatarAsync(command.file);
            user.userAvatar = avatarUrl;

            // 4. Persist the updated entity
            var result = await _userRepository.Update(user);

            if (result == 1)
            {
                return ResultUtils.success(data: user.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
