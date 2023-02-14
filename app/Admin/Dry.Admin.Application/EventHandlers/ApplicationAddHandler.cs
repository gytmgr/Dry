namespace Dry.Admin.Application.EventHandlers;

public class ApplicationAddHandler : INotificationHandler<ApplicationAddEvent>
{
    public ApplicationAddHandler(IServiceProvider serviceProvider)
    {
    }

    public async Task Handle(ApplicationAddEvent notification, CancellationToken cancellationToken)
    {
        await LogHelper.ActionAsync("fdsfdas");
    }
}