using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Dry.Mvc.Extensions
{
    /// <summary>
    /// http状态扩展
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string[] GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToArray();
        }
    }
}