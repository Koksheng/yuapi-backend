using MediatR;
using yuapi.Domain.MenuAggregate.Events;

namespace yuapi.Application.Menus.Events
{
    public class DummyHandler : INotificationHandler<MenuCreated>
    {
        public Task Handle(MenuCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
