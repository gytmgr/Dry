namespace Dry.Core.Utilities
{
    /// <summary>
    /// 结构扩展
    /// </summary>
    public static class StructExtension
    {
        /// <summary>
        /// 将默认值转成null
        /// </summary>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TStruct? DefaultToNull<TStruct>(this TStruct value) where TStruct : struct
        {
            if (value.Equals(default(TStruct)))
            {
                return null;
            }
            return value;
        }
    }
}