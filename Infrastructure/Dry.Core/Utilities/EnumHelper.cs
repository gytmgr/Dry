﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取指定枚举项说明
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, bool nameInstead = true)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            var field = type.GetField(name);
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }

        /// <summary>
        /// 获取指定类型枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetEnumDic<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "EnumType");
            }
            foreach (Enum enumValue in Enum.GetValues(type))
            {
                yield return new KeyValuePair<int, string>(Convert.ToInt32(enumValue), enumValue.GetDescription());
            }
        }
    }
}