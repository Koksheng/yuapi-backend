using Microsoft.AspNetCore.Http;

namespace yuapi.Application.Common.Interfaces.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAvatarAsync(IFormFile file);
    }
}
