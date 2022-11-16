namespace Dry.Quartz.Model
{
    /// <summary>
    /// Cron表达式触发器
    /// </summary>
    public class CronTriggerModel : TriggerModel
    {
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        public override string Check()
        {
            if (string.IsNullOrWhiteSpace(CronExpression))
            {
                return "Cron表达式必须录入";
            }
            return base.Check();
        }
    }
}