using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dry.Cache;

/// <summary>
/// 缓存扩展
/// </summary>
public static class MemoryCacheExtension
{
    /// <summary>
    /// 进程内加锁（阻塞）执行
    /// </summary>
    /// <param name="memoryCache"></param>
    /// <param name="executeFunc">执行方法</param>
    /// <param name="cacheKey">缓存主键</param>
    /// <param name="cacheExpirationSeconds">缓存失效时间（秒）</param>
    /// <returns></returns>
    public static async Task LockExecuteAsync(this IMemoryCache memoryCache, Func<Task> executeFunc, object cacheKey, double cacheExpirationSeconds = 10)
    {
        var lockObj = await memoryCache.GetOrCreateAsync(cacheKey, cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheExpirationSeconds);
            cacheEntry.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration
            {
                EvictionCallback = (object key, object value, EvictionReason reason, object state) =>
                {
                    if (value is IDisposable dis)
                    {
                        dis.Dispose();
                    }
                }
            });
            return Task.FromResult(new SemaphoreSlim(1, 1));
        });
        await lockObj.WaitAsync();
        try
        {
            await executeFunc();
        }
        finally
        {
            lockObj.Release();
        }
    }
}