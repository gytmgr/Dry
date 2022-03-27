using Dry.Core.Model;

namespace Dry.NPOI
{
    /// <summary>
    /// Excel操作异常
    /// </summary>
    public class ExcelBizException : BizException<ExcelExceptionCode>
    {
        /// <summary>
        /// 列索引
        /// </summary>
        public int? ColumnIndex { get; set; }

        /// <summary>
        /// 行索引
        /// </summary>
        public int? RowIndex { get; set; }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public ExcelBizException(ExcelExceptionCode code, string msg) : base(code, msg) { }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public ExcelBizException(ExcelExceptionCode code, string msg, int? columnIndex, int? rowIndex) : this(code, msg)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }
    }
}