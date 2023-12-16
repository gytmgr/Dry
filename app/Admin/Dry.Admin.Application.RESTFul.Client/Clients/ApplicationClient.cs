namespace Dry.Admin.Application.RESTFul.Client.Clients;

public class ApplicationClient : ApplicationQueryClientBase<ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService, IDependency<IApplicationAppService>
{
    protected override IClientRequestConfigurer RequestConfigurer => _serviceProvider.GetService<ClientRequestConfigurer>();

    protected override string ApiRelativeUrl => "/Api/Application";

    public ApplicationClient(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 获取应用类型
    /// </summary>
    /// <returns></returns>
    public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        => await RequestAsync<KeyValuePair<int, string>[]>(HttpMethod.Get, "/Type");
}