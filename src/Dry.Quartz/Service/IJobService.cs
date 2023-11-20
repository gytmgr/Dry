namespace Dry.Quartz.Service;

/// <summary>
/// 作业服务接口
/// </summary>
public interface IJobService
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
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="job"></param>
    /// <param name="triggers"></param>
    /// <returns></returns>
    Task<bool> AddAsync<TJobModel, TTriggerModel, TJob>(TJobModel job, params TTriggerModel[] triggers)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TJob : JobBase<TJobModel, TTriggerModel>;

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
    /// 立即执行一次
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <param name="job"></param>
    /// <returns></returns>
    Task<bool> ExecuteAsync<TJobModel>(TJobModel job)
        where TJobModel : JobModel;

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <returns></returns>
    Task<TJobModel[]> GetAsync<TJobModel>()
        where TJobModel : JobModel, new();

    /// <summary>
    /// 查询组
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <param name="group"></param>
    /// <returns></returns>
    Task<TJobModel[]> GetByGroupAsync<TJobModel>(string group)
        where TJobModel : JobModel, new();

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<TJobModel?> GetAsync<TJobModel>(QuartzKey key)
        where TJobModel : JobModel, new();

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<TTriggerModel[]> GetTriggerAsync<TTriggerModel>(QuartzKey key)
        where TTriggerModel : TriggerModel, new();
}