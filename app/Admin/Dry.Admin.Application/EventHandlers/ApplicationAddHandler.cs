namespace Dry.Admin.Application.EventHandlers;

public class ApplicationAddHandler : INotificationHandler<ApplicationAddEvent>
{
    public async Task Handle(ApplicationAddEvent notification, CancellationToken cancellationToken)
    {
        await LogHelper.ActionAsync("fdsfdas");
    }
}