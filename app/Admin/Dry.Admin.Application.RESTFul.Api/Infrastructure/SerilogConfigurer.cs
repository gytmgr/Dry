namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class SerilogConfigurer : IAppBuilderConfigurer
{
    public int Order { get; set; } = int.MaxValue;

    public Task ConfigureAsync(WebApplicationBuilder builder)
    {
        builder.Host.UseDrySerilog();
        return Task.CompletedTask;
    }
}