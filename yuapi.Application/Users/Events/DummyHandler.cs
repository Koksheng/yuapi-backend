using MediatR;
using yuapi.Domain.UserAggregate.Events;

namespace yuapi.Application.Users.Events
{
    public class DummyHandler : INotificationHandler<UserCreated>
    {
        public Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
