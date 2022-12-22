namespace Dry.Quartz.Service;

/// <summary>
/// 触发器服务接口
/// </summary>
public interface ITriggerService
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(QuartzKey key);

    /// <summary>
    /// 新增
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="jobKey"></param>
    /// <param name="trigger"></param>
    /// <returns></returns>
    Task<bool> AddAsync<TTriggerModel>(QuartzKey jobKey, TTriggerModel trigger)
        where TTriggerModel : TriggerModel;

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(QuartzKey key);

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task PauseAsync(QuartzKey key);

    /// <summary>
    /// 暂停组
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    Task PauseAsync(string group);

    /// <summary>
    /// 恢复
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task ResumeAsync(QuartzKey key);

    /// <summary>
    /// 恢复组
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    Task ResumeAsync(string group);

    /// <summary>
    /// 替换
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="oldTriggerKey"></param>
    /// <param name="newTrigger"></param>
    /// <returns></returns>
    Task<bool> ReplaceAsync<TTriggerModel>(QuartzKey oldTriggerKey, TTriggerModel newTrigger)
        where TTriggerModel : TriggerModel;

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <returns></returns>
    Task<TTriggerModel[]> GetAsync<TTriggerModel>()
        where TTriggerModel : TriggerModel, new();

    /// <summary>
    /// 查询组
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="group"></param>
    /// <returns></returns>
    Task<TTriggerModel[]> GetByGroupAsync<TTriggerModel>(string group)
        where TTriggerModel : TriggerModel, new();

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<TTriggerModel> GetAsync<TTriggerModel>(QuartzKey key)
        where TTriggerModel : TriggerModel, new();

    /// <summary>
    /// 查询状态
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<TriggerState> GetStateAsync(QuartzKey key);
}