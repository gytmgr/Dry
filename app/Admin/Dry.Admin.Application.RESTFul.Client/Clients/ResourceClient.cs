namespace Dry.Admin.Application.RESTFul.Client.Clients;

public class ResourceClient(IServiceProvider serviceProvider) : ApplicationQueryClientBase<ResourceDto, ResourceQueryDto, Guid>(serviceProvider), IResourceAppService, IDependency<IResourceAppService>
{
    protected override IClientRequestConfigurer RequestConfigurer => _serviceProvider.GetService<ClientRequestConfigurer>();

    protected override string ApiRelativeUrl => "/Api/Resource";
}