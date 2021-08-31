using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取指定枚举项说明
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead">是否用名称代替（没有说明时）</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, bool nameInstead = true)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name is null)
            {
                return null;
            }

            var field = type.GetField(name);
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute is null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }
    }

    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取指定类型枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetEnumDic<T>() where T : Enum
        {
            var type = typeof(T);
            foreach (Enum enumValue in Enum.GetValues(type))
            {
                yield return new KeyValuePair<int, string>(Convert.ToInt32(enumValue), enumValue.GetDescription());
            }
        }
    }
}