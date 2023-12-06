namespace Dry.Admin.Application.RESTFul.Client.Clients;

public class ApplicationClient : ApplicationQueryClientBase<ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService, IDependency<IApplicationAppService>
{
    protected override string ApiUrl => $"{AdminClientStatic.ApiUrl}/Api/Application";

    public ApplicationClient(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 获取应用类型
    /// </summary>
    /// <returns></returns>
    public async Task<KeyValuePair<int, string>[]> TypeArrayAsync()
        => await RequestAsync<KeyValuePair<int, string>[]>(HttpMethod.Get, "/Type");
}