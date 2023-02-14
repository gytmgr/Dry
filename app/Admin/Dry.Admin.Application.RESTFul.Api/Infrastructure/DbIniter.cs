namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class DbIniter : IAppConfigurer, ISingletonDependency<IAppConfigurer>
{
    public int Order { get; set; } = int.MinValue + 100;

    public virtual async Task ConfigureAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<IAdminContext>() as DbContext;
        await context.Database.MigrateAsync();
    }
}