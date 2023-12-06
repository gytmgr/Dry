namespace Dry.Core.Utilities;

/// <summary>
/// 异步帮助类
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    /// 分片异步执行
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="splitParams"></param>
    /// <param name="splitCount"></param>
    /// <param name="executeMethod"></param>
    /// <returns></returns>
    public static async Task<TResult[]> SplitExecuteAsync<TParam, TResult>(TParam[] splitParams, int splitCount, Func<TParam[], Task<TResult[]>> executeMethod)
    {
        var result = new ConcurrentBag<TResult[]>();
        var tasks = new Collection<Task>();
        for (int i = 0; i <= splitParams.Length / splitCount; i++)
        {
            var currentSplitParams = splitParams.Skip(i * splitCount).Take(splitCount).ToArray();
            if (currentSplitParams.Length > 0)
            {
                var task = executeMethod(currentSplitParams).ContinueWith(x => result.Add(x.Result));
                tasks.Add(task);
            }
        }
        await Task.WhenAll(tasks);
        return result.SelectMany(x => x).ToArray();
    }
}