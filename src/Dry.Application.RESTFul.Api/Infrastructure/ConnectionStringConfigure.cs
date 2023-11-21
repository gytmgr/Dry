﻿namespace Dry.Application.RESTFul.Api.Infrastructure;

public abstract class ConnectionStringConfigure<TBoundedContext> : IAppConfigurer, ISingletonDependency<IAppConfigurer> where TBoundedContext : IBoundedContext
{
    protected virtual string ConnectionStringKey => "default";

    public virtual int Order { get; set; } = default;

    public virtual async Task ConfigureAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<IDomainApplicationService<TBoundedContext>>().DbConnectionStringSetAsync(app.Services.GetService<IConfiguration>().GetConnectionString(ConnectionStringKey));
    }
}