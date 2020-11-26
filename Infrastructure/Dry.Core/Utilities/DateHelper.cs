using System;
using System.Globalization;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 获取中文星期
        /// </summary>
        /// <param name="solarDateTime"></param>
        /// <returns></returns>
        public static string SolarToChineseWeek(DateTime solarDateTime)
        {
            switch (solarDateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return "星期日";
            }
        }

        /// <summary>
        /// 公历转为农历的函数
        /// </summary>
        /// <param name="solarDateTime"></param>
        /// <returns></returns>
        public static string SolarToChineseLunisolarDate(DateTime solarDateTime)
        {
            var cal = new ChineseLunisolarCalendar();
            var year = cal.GetYear(solarDateTime);
            var month = cal.GetMonth(solarDateTime);
            var day = cal.GetDayOfMonth(solarDateTime);
            var leapMonth = cal.GetLeapMonth(year);
            return string.Format("农历{0}{1}（{2}）年{3}{4}月{5}{6}"
                                , "甲乙丙丁戊己庚辛壬癸"[(year - 4) % 10]
                                , "子丑寅卯辰巳午未申酉戌亥"[(year - 4) % 12]
                                , "鼠牛虎兔龙蛇马羊猴鸡狗猪"[(year - 4) % 12]
                                , month == leapMonth ? "闰" : ""
                                , "无正二三四五六七八九十冬腊"[leapMonth > 0 && leapMonth <= month ? month - 1 : month]
                                , "初十廿三"[day / 10]
                                , "日一二三四五六七八九"[day % 10]
                                );
        }
    }
}