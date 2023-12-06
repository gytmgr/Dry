namespace Dry.Dependency;

/// <summary>
/// 程序集扩展
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    /// 获取所有程序集
    /// </summary>
    /// <param name="prefixs">要筛选的程序集前缀</param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAll(params string[]? prefixs)
    {
        return DependencyContext.Default?.RuntimeLibraries
            .Where(x => prefixs is null or { Length: 0 } || prefixs.Any(y => x.Name.StartsWith(y)))
            .Select(x =>
             {
                 try
                 {
                     return Assembly.Load(new AssemblyName(x.Name));
                 }
                 catch
                 {
                     return null;
                 }
             })
            .Where(x => x is not null).Select(x => x!) ?? Enumerable.Empty<Assembly>();
    }
}