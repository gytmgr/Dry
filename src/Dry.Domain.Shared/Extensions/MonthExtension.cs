using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dry.Domain.Shared.Extensions;

/// <summary>
/// 月份扩展
/// </summary>
public static class MonthExtension
{
    /// <summary>
    /// 获取指定月份的季度
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    public static Quarter GetQuarter(this Month month)
        => (Quarter)(((byte)month - 1) / 3 + 1);
}