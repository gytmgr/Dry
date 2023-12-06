namespace Dry.Admin.Application.RESTFul.Api.Infrastructure;

public class ServiceConfigurer : IServiceConfigurer
{
    private readonly IServiceProvider _serviceProvider;

    public ServiceConfigurer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

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

        services.AddCustomAutoMapper();

        services.AddMediatR(AssemblyHelper.GetAll("Dry.").ToArray());

        return Task.CompletedTask;
    }
}