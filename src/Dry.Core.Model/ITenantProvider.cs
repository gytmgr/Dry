namespace Dry.Core.Model;

/// <summary>
/// 租户提供器接口
/// </summary>
public interface ITenantProvider : IHasId<string?>
{
    /// <summary>
    /// id存储键
    /// </summary>
    public const string IdKey = "TenantId";
}