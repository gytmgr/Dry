using System.ComponentModel;

namespace Dry.NPOI
{
    /// <summary>
    /// Excel操作异常编码
    /// </summary>
    public enum ExcelExceptionCode : byte
    {
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParamError,

        /// <summary>
        /// 文件错误
        /// </summary>
        [Description("文件错误")]
        FileError,

        /// <summary>
        /// 列标题错误
        /// </summary>
        [Description("列标题错误")]
        ColumnError,

        /// <summary>
        /// 行错误
        /// </summary>
        [Description("行错误")]
        RowError,

        /// <summary>
        /// 单元格错误
        /// </summary>
        [Description("单元格错误")]
        CellError
    }
}