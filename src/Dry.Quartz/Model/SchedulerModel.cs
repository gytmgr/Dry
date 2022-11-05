using System;

namespace Dry.Quartz.Model
{
    /// <summary>
    /// 调度模型
    /// </summary>
    public class SchedulerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 是否已启动
        /// </summary>
        public bool Started { get; internal set; }

        /// <summary>
        /// 是否待启动
        /// </summary>
        public bool InStandbyMode { get; internal set; }

        /// <summary>
        /// 是否已停止
        /// </summary>
        public bool Shutdown { get; internal set; }

        /// <summary>
        /// Quartz包版本
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime? RunningSince { get; internal set; }

        /// <summary>
        /// 已执行数量
        /// </summary>
        public int NumberOfJobsExecuted { get; internal set; }
    }
}