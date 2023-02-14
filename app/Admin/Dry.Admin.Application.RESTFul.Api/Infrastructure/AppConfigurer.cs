namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class AppConfigurer : IAppConfigurer, ISingletonDependency<IAppConfigurer>
{
    public int Order { get; set; }

    public Task ConfigureAsync(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCustomSwagger();

        app.UseRouting();

        app.UseCors("AllowOrigins");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return Task.CompletedTask;
    }
}