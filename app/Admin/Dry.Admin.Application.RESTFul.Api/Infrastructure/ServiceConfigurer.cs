namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class ServiceConfigurer : IServiceConfigurer, ISingletonDependency<IServiceConfigurer>
{
    private readonly IConfiguration _configuration;

    public ServiceConfigurer(IConfiguration configuration)
        => _configuration = configuration;

    public int Order { get; set; }

    public Task ConfigureAsync(IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddCustomSwagger();

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
            options.Filters.Add(typeof(ResourceFilter));
            options.Filters.Add(typeof(ActionFilter));
            options.Filters.Add(typeof(ResultFilter));
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

        services.AddAdminSqliteEFContext(_configuration.GetConnectionString("default"));

        services.AddCustomAutoMapper();

        services.AddEF();

        services.AddMediatR(AssemblyHelper.GetAll("Dry.").ToArray());

        return Task.CompletedTask;
    }
}