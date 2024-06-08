using yuapi.Application.Common.Interfaces.Services;

namespace yuapi.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
