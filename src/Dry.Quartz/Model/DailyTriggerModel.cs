using Quartz;
using System;
using System.Linq;

namespace Dry.Quartz.Model
{
    /// <summary>
    /// 日内间隔触发器
    /// </summary>
    public class DailyTriggerModel : TriggerModel
    {
        /// <summary>
        /// 每周执行日
        /// </summary>
        public DayOfWeek[] DayOfWeeks { get; set; }

        /// <summary>
        /// 每日开始执行时间
        /// </summary>
        public TimeOfDay StartTimeOfDay { get; set; }

        /// <summary>
        /// 每日结束执行时间
        /// </summary>
        public TimeOfDay EndTimeOfDay { get; set; }

        /// <summary>
        /// 日内执行间隔（秒）（1至86400]）
        /// </summary>
        public int IntervalSecond { get; set; } = 86400;

        /// <summary>
        /// 每日执行次数
        /// EndTimeOfDay有值时无效
        /// </summary>
        public int? CountOfDay { get; set; }

        /// <summary>
        /// 重复执行次数（-1：一直重复）
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        public override string Check()
        {
            if (DayOfWeeks?.Length > 0 && DayOfWeeks.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return "每周执行日有重复";
            }
            if (IntervalSecond is < 1 or > 86400)
            {
                return "日内执行间隔（秒）在1（1秒）到86400（1天）之间";
            }
            if (CountOfDay.HasValue)
            {
                if (CountOfDay < 1)
                {
                    return "每日执行次数必须大于1";
                }
                if (EndTimeOfDay is not null)
                {
                    return "每日结束执行时间有值时，每日重复执行次数无效";

                }
                if (StartTimeOfDay is null)
                {
                    return "当每日开始执行时间有值时，每日执行次数才有效";
                }
                var canMaxValue = (86400 - (StartTimeOfDay.Hour * 60 * 60 + StartTimeOfDay.Minute * 60 + StartTimeOfDay.Second)) / IntervalSecond - 1;
                if (CountOfDay > canMaxValue)
                {
                    return $"依据每日开始执行时间和执行间隔，每日执行次数最大可设置{canMaxValue}次";
                }
            }
            return base.Check();
        }
    }
}