using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// 对象克隆类
    /// </summary>
    public static class ObjClone
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType)
            {
                return obj;
            }
            else if (obj.GetType().IsSerializable)
            {
                object retval;
                using (MemoryStream ms = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    //序列化成流
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    //反序列化成对象
                    retval = bf.Deserialize(ms);
                    ms.Close();
                }
                return (T)retval;
            }
            else
            {

                var retval = Activator.CreateInstance(obj.GetType());
                var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var field in fields)
                {
                    try
                    {
                        field.SetValue(retval, DeepCopy(field.GetValue(obj)));
                    }
                    catch { }
                }
                return (T)retval;
            }
        }
    }
}