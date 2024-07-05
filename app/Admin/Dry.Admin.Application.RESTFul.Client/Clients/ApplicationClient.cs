namespace Dry.Admin.Application.RESTFul.Client.Clients;

public class ApplicationClient(IServiceProvider serviceProvider) : ApplicationQueryClientBase<ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>(serviceProvider), IApplicationAppService, IDependency<IApplicationAppService>
{
    protected override IClientRequestConfigurer RequestConfigurer => _serviceProvider.GetService<ClientRequestConfigurer>();

    protected override string ApiRelativeUrl => "/Api/Application";

    /// <summary>
    /// 获取应用类型
    /// </summary>
    /// <returns></returns>
    public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        => await RequestAsync<KeyValuePair<int, string>[]>(HttpMethod.Get, "/Type");
}