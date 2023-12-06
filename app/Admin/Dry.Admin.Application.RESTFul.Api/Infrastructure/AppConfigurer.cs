namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class AppConfigurer : ConnectionStringConfigureBase<IAdminContext>
{
    public override async Task ConfigureAsync(WebApplication app)
    {
        await base.ConfigureAsync(app);

        app.UseCustomSwagger();

        app.UseRouting();

        app.UseCors("AllowOrigins");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}