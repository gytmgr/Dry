namespace Dry.Admin.Application.RESTFul.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddDependency();

        services.AddCustomSwagger();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultConfig();
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
        });

        services.AddCors(options => options.AddPolicy("AllowOrigins", x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        ));

        services.AddAdminSqliteEFContext(Configuration.GetConnectionString("default"));

        services.AddCustomAutoMapper();

        services.AddEF();

        services.AddMediatR(AssemblyHelper.GetAll("Dry.").ToArray());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
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
    }
}