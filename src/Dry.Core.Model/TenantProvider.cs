namespace Dry.Core.Model;

/// <summary>
/// 租户提供器
/// </summary>
public class TenantProvider : ITenantProvider, IDependency<ITenantProvider>
{
    /// <summary>
    /// id
    /// </summary>
    public virtual string? Id { get; set; }
}