using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// url帮助类
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// 对象转url参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToUriParam(this object obj)
        {
            static List<string> GetObjectParam(object obj, string propertyName = null)
            {
                var propertis = obj.GetType().GetProperties();
                var result = new List<string>();
                foreach (var property in propertis)
                {
                    var name = string.IsNullOrEmpty(propertyName) ? property.Name : $"{propertyName}.{property.Name}";
                    var value = property.GetValue(obj, null);

                    if (value == null || value.Equals(property.PropertyType.DefaultValue()))
                    {
                        continue;
                    }
                    if (value is IEnumerable array && !(value is string))
                    {
                        var i = 0;
                        foreach (var item in array)
                        {
                            if (value.Equals(property.PropertyType.DefaultValue()))
                            {
                                continue;
                            }
                            if (item.GetType().IsValueType || item is string)
                            {
                                result.Add($"{name}[{i}]={HttpUtility.UrlEncode(item.ToString())}");
                            }
                            else
                            {
                                result.AddRange(GetObjectParam(item, $"{name}[{i}]"));
                            }
                            i++;
                        }
                    }
                    else
                    {
                        if (value.GetType().IsValueType || value is string)
                        {
                            result.Add($"{name}={HttpUtility.UrlEncode(value.ToString())}");
                        }
                        else
                        {
                            result.AddRange(GetObjectParam(value, name));
                        }
                    }
                }
                return result;
            }
            return string.Join("&", GetObjectParam(obj));
        }
    }
}