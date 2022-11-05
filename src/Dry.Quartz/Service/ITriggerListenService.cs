using Dry.Quartz.Infrastructure;
using Dry.Quartz.Model;

namespace Dry.Quartz.Service
{
    /// <summary>
    /// 触发器监听服务接口
    /// </summary>
    public interface ITriggerListenService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TJobModel"></typeparam>
        /// <typeparam name="TTriggerModel"></typeparam>
        /// <typeparam name="TTriggerListener"></typeparam>
        /// <param name="triggerListener"></param>
        /// <param name="triggerKeys"></param>
        void Add<TJobModel, TTriggerModel, TTriggerListener>(TTriggerListener triggerListener, params QuartzKey[] triggerKeys)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>;

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TJobModel"></typeparam>
        /// <typeparam name="TTriggerModel"></typeparam>
        /// <typeparam name="TTriggerListener"></typeparam>
        /// <param name="triggerListener"></param>
        /// <param name="triggerGroups"></param>
        void Add<TJobModel, TTriggerModel, TTriggerListener>(TTriggerListener triggerListener, params string[] triggerGroups)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Remove(string name);

        /// <summary>
        /// 增加触发器监听
        /// </summary>
        /// <param name="name"></param>
        /// <param name="triggerKeys"></param>
        void AddMatch(string name, params QuartzKey[] triggerKeys);

        /// <summary>
        /// 增加触发器组监听
        /// </summary>
        /// <param name="name"></param>
        /// <param name="triggerGroups"></param>
        void AddMatch(string name, params string[] triggerGroups);

        /// <summary>
        /// 移除触发器监听
        /// </summary>
        /// <param name="name"></param>
        /// <param name="triggerKey"></param>
        void RemoveMatch(string name, QuartzKey triggerKey);

        /// <summary>
        /// 移除触发器组监听
        /// </summary>
        /// <param name="name"></param>
        /// <param name="triggerGroup"></param>
        void RemoveMatch(string name, string triggerGroup);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TJobModel"></typeparam>
        /// <typeparam name="TTriggerModel"></typeparam>
        /// <typeparam name="TTriggerListener"></typeparam>
        /// <returns></returns>
        TTriggerListener[] Get<TJobModel, TTriggerModel, TTriggerListener>()
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>;

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TJobModel"></typeparam>
        /// <typeparam name="TTriggerModel"></typeparam>
        /// <typeparam name="TTriggerListener"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        TTriggerListener Get<TJobModel, TTriggerModel, TTriggerListener>(string name)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>;

        /// <summary>
        /// 触发器是否监听
        /// </summary>
        /// <param name="name"></param>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        bool IsMatch(string name, QuartzKey triggerKey);
    }
}