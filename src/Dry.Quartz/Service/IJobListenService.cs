namespace Dry.Quartz.Service;

/// <summary>
/// 作业监听服务接口
/// </summary>
public interface IJobListenService
{
    /// <summary>
    /// 添加
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TJobListener"></typeparam>
    /// <param name="jobListener"></param>
    /// <param name="jobKeys"></param>
    void Add<TJobModel, TTriggerModel, TJobListener>(TJobListener jobListener, params QuartzKey[] jobKeys)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TJobListener : JobListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 添加
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TJobListener"></typeparam>
    /// <param name="jobListener"></param>
    /// <param name="jobGroups"></param>
    void Add<TJobModel, TTriggerModel, TJobListener>(TJobListener jobListener, params string[] jobGroups)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TJobListener : JobListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    bool Remove(string name);

    /// <summary>
    /// 增加作业监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobKeys"></param>
    void AddMatch(string name, params QuartzKey[] jobKeys);

    /// <summary>
    /// 增加作业组监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobGroups"></param>
    void AddMatch(string name, params string[] jobGroups);

    /// <summary>
    /// 移除作业监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobKey"></param>
    void RemoveMatch(string name, QuartzKey jobKey);

    /// <summary>
    /// 移除作业组监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobGroup"></param>
    void RemoveMatch(string name, string jobGroup);

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TJobListener"></typeparam>
    /// <returns></returns>
    TJobListener[] Get<TJobModel, TTriggerModel, TJobListener>()
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TJobListener : JobListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TJobListener"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    TJobListener? Get<TJobModel, TTriggerModel, TJobListener>(string name)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TJobListener : JobListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 作业是否监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobKey"></param>
    /// <returns></returns>
    bool IsMatch(string name, QuartzKey jobKey);
}