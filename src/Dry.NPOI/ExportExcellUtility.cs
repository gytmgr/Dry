namespace Dry.NPOI;

/// <summary>
/// excel导出类
/// </summary>
public static class ExportExcellUtility
{
    private const string TEMPLATE_FOLDER = "Template";
    private const string OUTPUT_FOLDER = "Output";
    private const string COMPANY_NAME = "公司";
    private static readonly object _lockObject = new();
    private static readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;

    #region 扩展

    /// <summary>
    /// 单元格获取字符串值
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <returns></returns>
    public static string GetStringValue(this ICell cell)
        => cell.CellType switch
        {
            CellType.Numeric when DateUtil.IsCellDateFormatted(cell) => cell.DateCellValue.ToString(),
            CellType.Numeric => cell.NumericCellValue.ToString(),
            CellType.String => cell.StringCellValue?.Trim(),
            CellType.Boolean => cell.BooleanCellValue.ToString(),
            CellType.Formula => cell.CachedFormulaResultType switch
            {
                CellType.Numeric when DateUtil.IsCellDateFormatted(cell) => cell.DateCellValue.ToString(),
                CellType.Numeric => cell.NumericCellValue.ToString(),
                CellType.String => cell.StringCellValue?.Trim(),
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                _ => null
            },
            _ => null
        };

    /// <summary>
    /// 单元格设值
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="value">值</param>
    public static void SetValue(this ICell cell, object value)
    {
        if (value is not null)
        {
            switch (value)
            {
                case DateTime dtValue:
                    cell.SetCellValue(dtValue);
                    break;
                case bool boolValue:
                    cell.SetCellValue(boolValue);
                    break;
                case double doubleValue:
                    cell.SetCellValue(doubleValue);
                    break;
                default:
                    cell.SetCellValue(Convert.ToString(value));
                    break;
            }
        }
    }

    /// <summary>
    /// 获取或创建单元格
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="columnIndex">列索引</param>
    /// <param name="rowIndex">行索引</param>
    /// <returns></returns>
    public static ICell GetOrCreateCell(this ISheet sheet, int columnIndex, int rowIndex)
    {
        var row = sheet.GetRow(rowIndex);
        if (row is null)
        {
            row = sheet.CreateRow(rowIndex);
        }
        var cell = row.GetCell(columnIndex);
        if (cell is null)
        {
            cell = row.CreateCell(columnIndex);
        }
        return cell;
    }

    /// <summary>
    /// 单元格设值
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="columnIndex">列索引</param>
    /// <param name="rowIndex">行索引</param>
    /// <param name="value">值</param>
    public static void SetCellValue(this ISheet sheet, int columnIndex, int rowIndex, object value)
    {
        var cell = sheet.GetOrCreateCell(columnIndex, rowIndex);
        cell.SetValue(value);
    }

    /// <summary>
    /// 单元格设值
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="cellPoint">单元格位置</param>
    /// <param name="value">值</param>
    public static void SetCellValue(this ISheet sheet, Point cellPoint, object value)
    {
        var cell = sheet.GetOrCreateCell(cellPoint.Y, cellPoint.X);
        cell.SetValue(value);
    }

    /// <summary>
    /// 单元格设值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sheet">工作表</param>
    /// <param name="cellPoint">单元格位置</param>
    /// <param name="obj">设值对象</param>
    /// <param name="fieldName">设值字段名称</param>
    public static void SetCellValue<T>(this ISheet sheet, Point cellPoint, T obj, string fieldName)
    {
        if (obj is not null)
        {
            var property = obj.GetType().GetProperty(fieldName);
            if (property is not null)
            {
                sheet.SetCellValue(cellPoint, property.GetValue(obj));
            }
        }
    }

    /// <summary>
    /// 工作表设置数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sheet">工作表</param>
    /// <param name="data">数据</param>
    /// <param name="columns">列信息</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void SetData<T>(this ISheet sheet, T[] data, Column[] columns, Point contentCellStartPoint, int? columnRowStartIndex = default)
    {
        CheckColumn<T>(columns);
        var columnStartIndex = contentCellStartPoint.Y;
        if (columnRowStartIndex.HasValue)
        {
            foreach (var column in columns)
            {
                sheet.SetCellValue(contentCellStartPoint.Y, columnRowStartIndex.Value, column.Title);
                contentCellStartPoint.Y++;
            }
        }
        contentCellStartPoint.Y = columnStartIndex;
        foreach (var item in data)
        {
            foreach (var column in columns)
            {
                sheet.SetCellValue(contentCellStartPoint, item, column.Field);
                contentCellStartPoint.Y++;
            }
            contentCellStartPoint.X++;
            contentCellStartPoint.Y = columnStartIndex;
        }
    }

    /// <summary>
    /// 工作表设置数据
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="data">数据</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void SetData(this ISheet sheet, DataTable data, Point contentCellStartPoint, int? columnRowStartIndex = default)
    {
        var columnStartIndex = contentCellStartPoint.Y;
        if (columnRowStartIndex.HasValue)
        {
            foreach (DataColumn column in data.Columns)
            {
                sheet.SetCellValue(contentCellStartPoint.Y, columnRowStartIndex.Value, column.ColumnName);
                contentCellStartPoint.Y++;
            }
        }
        contentCellStartPoint.Y = columnStartIndex;
        foreach (DataRow row in data.Rows)
        {
            foreach (DataColumn column in data.Columns)
            {
                sheet.SetCellValue(contentCellStartPoint, row[column]);
                contentCellStartPoint.Y++;
            }
            contentCellStartPoint.X++;
            contentCellStartPoint.Y = columnStartIndex;
        }
    }

    /// <summary>
    /// 工作表设置数据
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="data">数据</param>
    public static void SetData(this ISheet sheet, Dictionary<Point, object> data)
    {
        foreach (var item in data)
        {
            sheet.SetCellValue(item.Key, item.Value);
        }
    }

    /// <summary>
    /// 工作表获取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sheet">工作表</param>
    /// <param name="columns">列信息</param>
    /// <param name="columnRowIndex">列所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行开始索引</param>
    /// <returns></returns>
    public static T[] GetData<T>(this ISheet sheet, Column[] columns, int columnRowIndex, int contentRowStartIndex) where T : class, new()
    {
        CheckColumn<T>(columns);
        var columnRow = sheet.GetRow(columnRowIndex);
        if (columnRow is null)
        {
            throw new ExcelBizException(ExcelExceptionCode.RowError, $"列所在【行：{columnRowIndex + 1}】不存在", columnRowIndex, default);
        }
        if (columnRow.FirstCellNum == -1)
        {
            throw new ExcelBizException(ExcelExceptionCode.RowError, $"列所在【行：{columnRowIndex + 1}】没有数据", columnRowIndex, default);
        }
        var existColumns = new List<Column>();
        for (int i = columnRow.FirstCellNum; i < columnRow.LastCellNum; i++)
        {
            var cell = columnRow.GetCell(i);
            if (cell != null)
            {
                var columnName = cell.GetStringValue();
                if (columnName?.Length > 0)
                {
                    var column = columns.FirstOrDefault(x => x.Title == columnName);
                    if (column is not null)
                    {
                        if (column.Index.HasValue)
                        {
                            if (column.Index != i)
                            {
                                throw new ExcelBizException(ExcelExceptionCode.CellError, $"【行：{columnRowIndex + 1}，列：{column.Title}】的【列索引：{i + 1}】错误，应为【列索引：{column.Index + 1}】", columnRowIndex, i);
                            }
                        }
                        else
                        {
                            column.Index = i;
                        }
                        existColumns.Add(column);
                    }
                }
            }
        }
        var notExistColumns = columns.Except(existColumns, (x, y) => x.Title == y.Title).ToArray();
        if (notExistColumns.Length > 0)
        {
            throw new ExcelBizException(ExcelExceptionCode.ColumnError, $"【列：{string.Join("，", notExistColumns.Select(x => x.Title))}】不存在", columnRowIndex, default);
        }
        var data = new List<T>();
        for (int i = contentRowStartIndex; i <= sheet.LastRowNum; i++)
        {
            var row = sheet.GetRow(i);
            var item = new T();

            foreach (var column in columns)
            {
                var cell = row.GetCell(column.Index.Value);
                if (cell is not null)
                {
                    if (cell is { CellType: CellType.Error } or { CellType: CellType.Formula, CachedFormulaResultType: CellType.Error })
                    {
                        throw new ExcelBizException(ExcelExceptionCode.CellError, $"【行：{i + 1}，列：{column.Title}】有错误", i, column.Index.Value);
                    }
                    var cellValue = cell.GetStringValue();
                    if (cellValue is null)
                    {
                        if (column.Required)
                        {
                            throw new ExcelBizException(ExcelExceptionCode.CellError, $"【行：{i + 1}，列：{column.Title}】必须录入", i, column.Index.Value);
                        }
                    }
                    else
                    {
                        var property = item.GetType().GetProperty(column.Field);
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(item, cellValue);
                        }
                        else
                        {
                            if (cellValue.TryParse(property.PropertyType, out object propertyValue))
                            {
                                property.SetValue(item, propertyValue);
                            }
                            else
                            {
                                throw new ExcelBizException(ExcelExceptionCode.CellError, $"【行：{i + 1}，列：{column.Title}】数据类型错误", i, column.Index.Value);
                            }
                        }
                    }
                }
                else
                {
                    if (column.Required)
                    {
                        throw new ExcelBizException(ExcelExceptionCode.CellError, $"【行：{i + 1}，列：{column.Title}】必须录入", i, column.Index.Value);
                    }
                }
            }

            data.Add(item);
        }
        return data.ToArray();
    }

    /// <summary>
    /// 工作表获取数据
    /// </summary>
    /// <param name="sheet">工作表</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行的开始索引</param>
    /// <returns></returns>
    public static DataTable GetData(this ISheet sheet, int? columnRowIndex = default, int? contentRowStartIndex = default)
    {
        var dt = new DataTable();
        var columns = new List<(string columnName, int excelColumnIndex)>();
        if (columnRowIndex.HasValue)
        {
            var columnRow = sheet.GetRow(columnRowIndex.Value);
            if (columnRow is null)
            {
                throw new ExcelBizException(ExcelExceptionCode.RowError, $"列所在【行：{columnRowIndex + 1}】不存在", columnRowIndex, default);
            }
            if (columnRow.FirstCellNum == -1)
            {
                throw new ExcelBizException(ExcelExceptionCode.RowError, $"列所在【行：{columnRowIndex + 1}】没有数据", columnRowIndex, default);
            }
            for (int i = columnRow.FirstCellNum; i < columnRow.LastCellNum; i++)
            {
                var cell = columnRow.GetCell(i);
                if (cell != null)
                {
                    var columnName = cell.GetStringValue();
                    if (columnName is null or { Length: 0 })
                    {
                        continue;
                    }
                    dt.Columns.Add(columnName);
                    columns.Add((columnName, i));
                }
            }
        }
        for (int i = contentRowStartIndex ?? sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            var row = sheet.GetRow(i);
            if (row is null or { FirstCellNum: -1 })
            {
                continue;
            }
            var dr = dt.NewRow();
            var isEmptyRow = true;
            for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
            {
                var cell = row.GetCell(j);
                if (cell is null)
                {
                    continue;
                }
                if (columnRowIndex.HasValue)
                {
                    var column = columns.Find(x => x.excelColumnIndex == j);
                    if (column == default)
                    {
                        continue;
                    }
                    dr[column.columnName] = cell.GetStringValue();
                }
                else
                {
                    while (dt.Columns.Count <= j)
                    {
                        dt.Columns.Add();
                    }
                    dr[j] = cell.GetStringValue();
                }
                isEmptyRow = false;
            }
            if (!isEmptyRow)
            {
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    /// <summary>
    /// 获取或创建工作表
    /// </summary>
    /// <param name="workbook">工作薄</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static ISheet GetOrCreateSheet(this IWorkbook workbook, string sheetName = default)
    {
        ISheet sheet;
        if (string.IsNullOrEmpty(sheetName))
        {
            if (workbook.NumberOfSheets > 0)
            {
                sheet = workbook.GetSheetAt(0);
            }
            else
            {
                sheet = workbook.CreateSheet();
            }
        }
        else
        {
            sheet = workbook.GetSheet(sheetName);
            sheet ??= workbook.CreateSheet(sheetName);
        }
        return sheet;
    }

    /// <summary>
    /// 获取命名或者第一个工作表
    /// </summary>
    /// <param name="workbook">工作薄</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static ISheet GetNamedOrFirstSheet(this IWorkbook workbook, string sheetName = default)
    {
        var sheet = string.IsNullOrEmpty(sheetName) ? workbook.GetSheetAt(0) : workbook.GetSheet(sheetName);
        if (sheet is null)
        {
            throw new ExcelBizException(ExcelExceptionCode.FileError, $"【工作表：{sheetName}】不存在");
        }
        return sheet;
    }

    /// <summary>
    /// 工作薄设置数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="workbook">工作薄</param>
    /// <param name="data">数据</param>
    /// <param name="columns">列信息</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void SetData<T>(this IWorkbook workbook, T[] data, Column[] columns, Point contentCellStartPoint, string sheetName = default, int? columnRowStartIndex = default)
    {
        var sheet = workbook.GetOrCreateSheet(sheetName);
        sheet.SetData(data, columns, contentCellStartPoint, columnRowStartIndex);
    }

    /// <summary>
    /// 工作薄设置数据
    /// </summary>
    /// <param name="workbook">工作薄</param>
    /// <param name="data">数据</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void SetData(this IWorkbook workbook, DataTable data, Point contentCellStartPoint, string sheetName = default, int? columnRowStartIndex = default)
    {
        var sheet = workbook.GetOrCreateSheet(sheetName);
        sheet.SetData(data, contentCellStartPoint, columnRowStartIndex);
    }

    /// <summary>
    /// 工作薄获取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="workbook">工作薄</param>
    /// <param name="columns">列信息</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行开始索引</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static T[] GetData<T>(this IWorkbook workbook, Column[] columns, int columnRowIndex, int contentRowStartIndex, string sheetName = default) where T : class, new()
    {
        var sheet = workbook.GetNamedOrFirstSheet(sheetName);
        return sheet.GetData<T>(columns, columnRowIndex, contentRowStartIndex);
    }

    /// <summary>
    /// 工作薄获取数据
    /// </summary>
    /// <param name="workbook">工作薄</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行的开始索引</param>
    /// <returns></returns>
    public static DataTable GetData(this IWorkbook workbook, string sheetName = default, int? columnRowIndex = default, int? contentRowStartIndex = default)
    {
        var sheet = workbook.GetNamedOrFirstSheet(sheetName);
        return sheet.GetData(columnRowIndex, contentRowStartIndex);
    }

    /// <summary>
    /// 工作薄转内存
    /// </summary>
    /// <param name="workbook">工作薄</param>
    /// <returns></returns>
    public static MemoryStream ToMemoryStream(this IWorkbook workbook)
    {
        var ms = new MemoryStream();
        workbook.Write(ms, false);
        return ms;
    }

    #endregion

    /// <summary>
    /// 检查列信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="columns">列信息</param>
    public static void CheckColumn<T>(Column[] columns)
    {
        foreach (var column in columns)
        {
            if (column.Field is null or { Length: 0 })
            {
                throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：列信息】必须录入【Field】字段");
            }
            if (typeof(T).GetProperty(column.Field) is null)
            {
                throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：列信息】对象没有【Field】字段");
            }
            if (column.Title is null or { Length: 0 })
            {
                column.Title = column.Field;
            }
        }
        if (columns.GroupBy(x => x.Field).Any(x => x.Count() > 1))
        {
            throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：列信息】字段【Field】有重复");
        }
        if (columns.GroupBy(x => x.Title).Any(x => x.Count() > 1))
        {
            throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：列信息】字段【Title】有重复");
        }
    }

    /// <summary>
    /// 获取工作薄
    /// </summary>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <param name="stream">文件流</param>
    /// <returns></returns>
    public static IWorkbook GetWorkbook(bool isXlsx = false, Stream stream = default)
    {
        if (stream is null)
        {
            return isXlsx ? new XSSFWorkbook() : new HSSFWorkbook();
        }
        else
        {
            return isXlsx ? new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
        }
    }

    /// <summary>
    /// 对象转Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objs">对象列表</param>
    /// <param name="columns">列信息</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    /// <param name="templateFileStream">模板文件流</param>
    /// <returns></returns>
    public static MemoryStream ObjectToExcel<T>(T[] objs, Column[] columns, Point contentCellStartPoint, bool isXlsx = false, string sheetName = default, int? columnRowStartIndex = default, Stream templateFileStream = default) where T : class
    {
        var workbook = GetWorkbook(isXlsx, templateFileStream);
        try
        {
            workbook.SetData(objs, columns, contentCellStartPoint, sheetName, columnRowStartIndex);
            return workbook.ToMemoryStream();
        }
        finally
        {
            workbook?.Close();
        }
    }

    /// <summary>
    /// 对象转Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objs">对象列表</param>
    /// <param name="columns">列信息</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="templateFilePath">模板文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    /// <returns></returns>
    public static MemoryStream ObjectToExcel<T>(T[] objs, Column[] columns, Point contentCellStartPoint, string templateFilePath, string sheetName = default, int? columnRowStartIndex = default) where T : class
    {
        var fs = default(FileStream);
        if (!string.IsNullOrEmpty(templateFilePath))
        {
            if (!File.Exists(templateFilePath))
            {
                throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：模板文件路径】找不到文件");
            }
            fs = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        try
        {
            return ObjectToExcel(objs, columns, contentCellStartPoint, templateFilePath.EndsWith(".xlsx"), sheetName, columnRowStartIndex, fs);
        }
        finally
        {
            fs?.Close();
        }
    }

    /// <summary>
    /// DataTable转Excel
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    /// <param name="templateFileStream">模板文件流</param>
    /// <returns></returns>
    public static MemoryStream DataTableToExcel(DataTable dt, Point contentCellStartPoint, bool isXlsx = false, string sheetName = default, int? columnRowStartIndex = default, Stream templateFileStream = default)
    {
        var workbook = GetWorkbook(isXlsx, templateFileStream);
        try
        {
            workbook.SetData(dt, contentCellStartPoint, sheetName, columnRowStartIndex);
            return workbook.ToMemoryStream();
        }
        finally
        {
            workbook?.Close();
        }
    }

    /// <summary>
    /// DataTable转Excel
    /// </summary>
    /// <param name="dt">DataTable</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="templateFilePath">模板文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    /// <returns></returns>
    public static MemoryStream DataTableToExcel(DataTable dt, Point contentCellStartPoint, string templateFilePath, string sheetName = default, int? columnRowStartIndex = default)
    {
        var fs = default(FileStream);
        if (!string.IsNullOrEmpty(templateFilePath))
        {
            if (!File.Exists(templateFilePath))
            {
                throw new ExcelBizException(ExcelExceptionCode.ParamError, "【参数：模板文件路径】找不到文件");
            }
            fs = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        try
        {
            return DataTableToExcel(dt, contentCellStartPoint, templateFilePath.EndsWith(".xlsx"), sheetName, columnRowStartIndex, fs);
        }
        finally
        {
            fs?.Close();
        }
    }

    /// <summary>
    /// Excel转对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="excelFileStream">excel文件流</param>
    /// <param name="columns">列信息</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行开始索引</param>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static T[] ExcelToObject<T>(Stream excelFileStream, Column[] columns, int columnRowIndex, int contentRowStartIndex, bool isXlsx = false, string sheetName = default) where T : class, new()
    {
        var workbook = GetWorkbook(isXlsx, excelFileStream);
        try
        {
            return workbook.GetData<T>(columns, columnRowIndex, contentRowStartIndex, sheetName);
        }
        finally
        {
            workbook?.Close();
        }
    }

    /// <summary>
    /// Excel转对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="excelFilePath">excel文件路径</param>
    /// <param name="columns">列信息</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行开始索引</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static T[] ExcelToObject<T>(string excelFilePath, Column[] columns, int columnRowIndex, int contentRowStartIndex, string sheetName = default) where T : class, new()
    {
        var fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        try
        {
            return ExcelToObject<T>(fs, columns, columnRowIndex, contentRowStartIndex, excelFilePath.EndsWith(".xlsx"), sheetName);
        }
        finally
        {
            fs?.Close();
        }
    }

    /// <summary>
    /// Excel转DataTable
    /// </summary>
    /// <param name="excelFileStream">excel文件流</param>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行的开始索引</param>
    /// <returns></returns>
    public static DataTable ExcelToDataTable(Stream excelFileStream, bool isXlsx = default, string sheetName = default, int? columnRowIndex = default, int? contentRowStartIndex = default)
    {
        var workbook = GetWorkbook(isXlsx, excelFileStream);
        try
        {
            return workbook.GetData(sheetName, columnRowIndex, contentRowStartIndex);
        }
        finally
        {
            workbook?.Close();
        }
    }

    /// <summary>
    /// Excel转DataTable
    /// </summary>
    /// <param name="excelFilePath">excel文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行的开始索引</param>
    /// <returns></returns>
    public static DataTable ExcelToDataTable(string excelFilePath, string sheetName = default, int? columnRowIndex = default, int? contentRowStartIndex = default)
    {
        var fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        try
        {
            return ExcelToDataTable(fs, excelFilePath.EndsWith(".xlsx"), sheetName, columnRowIndex, contentRowStartIndex);
        }
        finally
        {
            fs?.Close();
        }
    }

    /// <summary>
    /// 写Excel文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="excelFilePath">excel文件路径</param>
    /// <param name="objs">对象列表</param>
    /// <param name="columns">列信息</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void Write<T>(string excelFilePath, T[] objs, Column[] columns, Point contentCellStartPoint, string sheetName = default, int? columnRowStartIndex = default)
    {
        using var readFileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var workbook = GetWorkbook(excelFilePath.EndsWith(".xlsx"), readFileStream);
        try
        {
            workbook.SetData(objs, columns, contentCellStartPoint, sheetName, columnRowStartIndex);
            using var writeFileStream = File.Create(excelFilePath);
            workbook.Write(writeFileStream, false);
            writeFileStream.Close();
        }
        finally
        {
            workbook?.Close();
            readFileStream.Close();
        }
    }

    /// <summary>
    /// 写Excel文件
    /// </summary>
    /// <param name="excelFilePath">excel文件路径</param>
    /// <param name="dt">DataTable</param>
    /// <param name="contentCellStartPoint">数据单元格开始位置</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="columnRowStartIndex">列所在行的开始索引</param>
    public static void Write(string excelFilePath, DataTable dt, Point contentCellStartPoint, string sheetName = default, int? columnRowStartIndex = default)
    {
        using var readFileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var workbook = GetWorkbook(excelFilePath.EndsWith(".xlsx"), readFileStream);
        try
        {
            workbook.SetData(dt, contentCellStartPoint, sheetName, columnRowStartIndex);
            using var writeFileStream = File.Create(excelFilePath);
            workbook.Write(writeFileStream, false);
            writeFileStream.Close();
        }
        finally
        {
            workbook?.Close();
            readFileStream.Close();
        }
    }

    /// <summary>
    /// excel转对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fs">文件流</param>
    /// <param name="columns">列信息</param>
    /// <param name="columnRowIndex">列名所在的行索引</param>
    /// <param name="contentRowStartIndex">数据行开始索引</param>
    /// <param name="isXlsx">是否xlsx版本</param>
    /// <returns></returns>
    public static Result<byte, T[]> ExcelToObj<T>(Stream fs, Column[] columns, int columnRowIndex, int contentRowStartIndex, bool isXlsx = false) where T : class, new()
    {
        try
        {
            var objs = ExcelToObject<T>(fs, columns, columnRowIndex, contentRowStartIndex, isXlsx, default);
            return Result<byte, T[]>.Create(1, objs);
        }
        catch (ExcelBizException ex)
        {
            return Result<byte, T[]>.Create(0, ex.Message, default);
        }
    }

    #region old source

    ///// <summary>
    ///// 导出带模板excel(显示时columns第一个元素new一个空对象)
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="items">导出数据集合</param>
    ///// <param name="columns">columns每列对应数据item</param>
    ///// <param name="templatePath">模板名称</param>
    ///// <param name="addStartIndex">开始加入数据行</param>
    ///// <param name="showSerialNumber">是否显示序号，显示时columns第一个元素new一个空对象</param>
    ///// <param name="title">Excel头</param>
    ///// <param name="showBorder">是否显示边框</param>
    ///// <param name="heightInPoints">行高默认为40</param>
    ///// <param name="fontName">内容字体默认为宋体</param>
    ///// <param name="fontSize">内容大小默认为12</param>
    ///// <param name="cells">指定单元格值</param>
    ///// <returns></returns>
    //public static MemoryStream ExportHasTemplatePath<T>(List<T> items, List<Column> columns, string templatePath, int addStartIndex, bool showSerialNumber,
    //    string title = null, bool showBorder = false, float heightInPoints = 20, string fontName = "宋体", short fontSize = 12, List<MyCell> cells = null)
    //{
    //    //绝对路径
    //    string templateFilePath = templatePath = Path.Combine(_basePath, TEMPLATE_FOLDER, templatePath);
    //    FileStream stream = new(templateFilePath, FileMode.Open);
    //    IWorkbook workBook = GetIWorkBook(templateFilePath, stream);
    //    ISheet sheet = workBook.GetSheetAt(0);

    //    if (!string.IsNullOrEmpty(title))
    //    {
    //        IRow row1 = sheet.GetRow(0);
    //        ICell cell1 = row1.GetCell(0);
    //        cell1.SetCellValue(title);
    //        ICellStyle style = workBook.CreateCellStyle();
    //        style.Alignment = HorizontalAlignment.Center;
    //        style.VerticalAlignment = VerticalAlignment.Center;
    //        IFont font = workBook.CreateFont();
    //        font.FontHeightInPoints = 15;
    //        font.Boldweight = 500;
    //        style.SetFont(font);
    //        cell1.CellStyle = style;
    //    }
    //    //
    //    if (cells != null && cells.Count > 0)
    //    {
    //        cells.ForEach(item =>
    //        {
    //            IRow row = sheet.GetRow(item.RowIndex);
    //            ICell cell = row.GetCell(item.ColIndex);
    //            cell.SetCellValue(item.Val);
    //        });
    //    }
    //    BuildItemsEx(workBook, workBook.GetSheetAt(0), columns, ref addStartIndex, items, showSerialNumber, showBorder, heightInPoints, fontName, fontSize);

    //    MemoryStream resultStream = new MemoryStream();
    //    workBook.Write(resultStream);
    //    return resultStream;
    //}

    ///// <summary>
    ///// 区分.xls，.xlsx文件生成IworkBook实例
    ///// </summary>
    ///// <param name="filePath">文件路径</param>
    ///// <param name="sourceStream">文件流</param>
    ///// <returns></returns>
    //public static IWorkbook GetIWorkBook(string filePath, Stream sourceStream)
    //{
    //    string fileExt = Path.GetExtension(filePath);
    //    switch (fileExt)
    //    {
    //        case ".xls":
    //            return new HSSFWorkbook(sourceStream);
    //        case ".xlsx":
    //            return new XSSFWorkbook(sourceStream);
    //        default:
    //            return null;
    //    }
    //}

    ///// <summary>
    ///// 导出EXCEL
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="title"></param>
    ///// <param name="items"></param>
    ///// <param name="columns"></param>
    ///// <param name="showSerialNumber">是否显示序号</param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <returns></returns>
    //public static MemoryStream Export<T>(string title, List<T> items, List<Column> columns, bool showSerialNumber = false, string fontName = "宋体", short fontSize = 12)
    //{
    //    IWorkbook workbook = Build<T>(title, items, columns, fontName, fontSize, showSerialNumber);
    //    MemoryStream stream = new MemoryStream();
    //    workbook.Write(stream);
    //    return stream;
    //}

    ///// <summary>
    ///// 导出EXCEL
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="data"></param>
    ///// <param name="showSerialNumber"></param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <param name="haveHeader"></param>
    ///// <returns></returns>
    //public static MemoryStream Export<T>(Tuple<string, List<T>, List<Column>>[] data, bool showSerialNumber = false, string fontName = "宋体", short fontSize = 12, bool haveHeader = false)
    //{
    //    var first = data.Length > 0 ? data[0] : Tuple.Create("Sheet1", new List<T>(), new List<Column>());
    //    var workbook = Build(first.Item1, first.Item2, first.Item3, fontName, fontSize, showSerialNumber, haveHeader);
    //    for (int i = 1; i < data.Length; i++)
    //    {
    //        Build((HSSFWorkbook)workbook, data[i].Item1, data[i].Item2, data[i].Item3, fontName, fontSize, showSerialNumber, haveHeader);
    //    }
    //    var stream = new MemoryStream();
    //    workbook.Write(stream);
    //    return stream;
    //}

    ///// <summary>
    ///// 创建工作簿
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="title"></param>
    ///// <param name="items"></param>
    ///// <param name="columns"></param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <param name="showSerialNumber"></param>
    ///// <param name="haveHeader"></param>
    ///// <returns></returns>
    //public static IWorkbook Build<T>(string title, List<T> items, List<Column> columns, string fontName, short fontSize, bool showSerialNumber, bool haveHeader = true)
    //{
    //    HSSFWorkbook workbook = new();
    //    ISheet sheet = workbook.CreateSheet(title);

    //    int currentRow = 0;

    //    #region excell文件附属信息

    //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
    //    dsi.Company = COMPANY_NAME;
    //    workbook.DocumentSummaryInformation = dsi;

    //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
    //    si.Author = COMPANY_NAME; //填加xls文件作者信息
    //    si.ApplicationName = COMPANY_NAME; //填加xls文件创建程序信息
    //    si.LastAuthor = COMPANY_NAME; //填加xls文件最后保存者信息
    //    si.Comments = COMPANY_NAME; //填加xls文件作者信息
    //    si.Title = COMPANY_NAME; //填加xls文件标题信息
    //    si.Subject = COMPANY_NAME;//填加文件主题信息
    //    si.CreateDateTime = DateTime.Now;
    //    workbook.SummaryInformation = si;

    //    #endregion

    //    #region excell文件表头

    //    if (haveHeader)
    //    {
    //        int columnsCount = columns.Count;
    //        if (showSerialNumber)
    //            columnsCount++;
    //        BuildHeader(workbook, sheet, title, ref currentRow, columnsCount);
    //    }

    //    #endregion

    //    #region excell文件列头

    //    BuildColumnHeader(workbook, sheet, ref currentRow, columns, showSerialNumber, fontName, fontSize);

    //    #endregion

    //    #region excell文件行

    //    BuildItems(workbook, sheet, columns, ref currentRow, items, fontName, fontSize, showSerialNumber);

    //    #endregion

    //    #region 设置列宽度

    //    // SetColumnWidth(sheet, columns.Count);

    //    #endregion

    //    return workbook;
    //}

    ///// <summary>
    ///// 在指定工作簿配置数据
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="workbook"></param>
    ///// <param name="title"></param>
    ///// <param name="items"></param>
    ///// <param name="columns"></param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <param name="showSerialNumber"></param>
    ///// <param name="haveHeader"></param>
    //public static void Build<T>(HSSFWorkbook workbook, string title, List<T> items, List<Column> columns, string fontName, short fontSize, bool showSerialNumber, bool haveHeader = true)
    //{
    //    ISheet sheet = workbook.CreateSheet(title);

    //    int currentRow = 0;

    //    #region excell文件附属信息

    //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
    //    dsi.Company = COMPANY_NAME;
    //    workbook.DocumentSummaryInformation = dsi;

    //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
    //    si.Author = COMPANY_NAME; //填加xls文件作者信息
    //    si.ApplicationName = COMPANY_NAME; //填加xls文件创建程序信息
    //    si.LastAuthor = COMPANY_NAME; //填加xls文件最后保存者信息
    //    si.Comments = COMPANY_NAME; //填加xls文件作者信息
    //    si.Title = COMPANY_NAME; //填加xls文件标题信息
    //    si.Subject = COMPANY_NAME;//填加文件主题信息
    //    si.CreateDateTime = DateTime.Now;
    //    workbook.SummaryInformation = si;

    //    #endregion

    //    #region excell文件表头

    //    if (haveHeader)
    //    {
    //        int columnsCount = columns.Count;
    //        if (showSerialNumber)
    //            columnsCount++;
    //        BuildHeader(workbook, sheet, title, ref currentRow, columnsCount);
    //    }

    //    #endregion

    //    #region excell文件列头

    //    BuildColumnHeader(workbook, sheet, ref currentRow, columns, showSerialNumber, fontName, fontSize);

    //    #endregion

    //    #region excell文件行

    //    BuildItems(workbook, sheet, columns, ref currentRow, items, fontName, fontSize, showSerialNumber);

    //    #endregion

    //    #region 设置列宽度

    //    // SetColumnWidth(sheet, columns.Count);

    //    #endregion

    //    //return workbook;
    //}

    //private static void SetColumnWidth(ISheet sheet, int colCount)
    //{
    //    //列宽自适应，只对英文和数字有效
    //    for (int i = 0; i <= colCount; i++)
    //    {
    //        //sheet.AutoSizeColumn(i);
    //    }
    //    //获取当前列的宽度，然后对比本列的长度，取最大值
    //    for (int columnNum = 0; columnNum <= colCount; columnNum++)
    //    {
    //        int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
    //        for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
    //        {
    //            IRow currentRow;
    //            //当前行未被使用过
    //            if (sheet.GetRow(rowNum) == null)
    //            {
    //                currentRow = sheet.CreateRow(rowNum);
    //            }
    //            else
    //            {
    //                currentRow = sheet.GetRow(rowNum);
    //            }

    //            if (currentRow.GetCell(columnNum) != null)
    //            {
    //                ICell currentCell = currentRow.GetCell(columnNum);
    //                int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
    //                if (columnWidth < length)
    //                {
    //                    columnWidth = length;
    //                }
    //            }
    //        }
    //        sheet.SetColumnWidth(columnNum, (columnWidth > 255 ? 255 : columnWidth) * 256);
    //    }
    //}

    //private static void BuildHeader(IWorkbook workbook, ISheet sheet, string title, ref int currentRow, int colnum)
    //{
    //    var row = sheet.CreateRow(currentRow++);
    //    row.HeightInPoints = 25;
    //    ICell cell = row.CreateCell(0);
    //    cell.SetCellValue(title);

    //    ICellStyle style = workbook.CreateCellStyle();
    //    style.Alignment = HorizontalAlignment.Center;
    //    IFont font = workbook.CreateFont();
    //    font.FontHeightInPoints = 20;
    //    font.Boldweight = 700;
    //    style.SetFont(font);
    //    cell.CellStyle = style;

    //    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, colnum - 1));
    //}

    //private static void BuildColumnHeader(IWorkbook workbook, ISheet sheet, ref int currentRow, List<Column> columns, bool showSerialNumber, string fontName, short fontSize)
    //{
    //    IRow row = sheet.CreateRow(currentRow++);
    //    ICellStyle style = workbook.CreateCellStyle();
    //    style.Alignment = HorizontalAlignment.Center;
    //    IFont font = workbook.CreateFont();
    //    font.FontHeightInPoints = fontSize;
    //    font.FontName = fontName;
    //    font.Boldweight = 700;
    //    style.SetFont(font);
    //    int columnStartIndex = 0;
    //    int columnCount = columns.Count;
    //    if (showSerialNumber)
    //    {
    //        columnStartIndex = 1;
    //        ICell cell = row.CreateCell(0);
    //        cell.CellStyle = style;
    //        cell.SetCellValue("序号");
    //        columnCount++;
    //        sheet.SetColumnWidth(columnStartIndex, 50 * 40);
    //    }
    //    for (int columnIndex = columnStartIndex; columnIndex < columnCount; columnIndex++)
    //    {
    //        ICell cell = row.CreateCell(columnIndex);
    //        cell.CellStyle = style;
    //        Column column;
    //        if (showSerialNumber)
    //            column = columns[columnIndex - 1];
    //        else
    //            column = columns[columnIndex];
    //        CellSetValue<Column>(cell, "title", column);
    //        sheet.SetColumnWidth(columnIndex, column.Width * 40);
    //    }
    //}

    //private static ICellStyle CreateCellStyle(IWorkbook workBook, Column column, bool showBorder)
    //{
    //    ICellStyle style = workBook.CreateCellStyle();
    //    style.VerticalAlignment = column.VValue;
    //    style.Alignment = column.HValue;
    //    if (showBorder)
    //    {
    //        style.BorderRight = BorderStyle.Thin;
    //        style.BorderLeft = BorderStyle.Thin;
    //        style.BorderTop = BorderStyle.Thin;
    //        style.BorderBottom = BorderStyle.Thin;
    //    }
    //    return style;
    //}

    //private static void BuildItemsEx<T>(IWorkbook workbook, ISheet sheet, List<Column> columns,
    //    ref int currentRow, IList<T> items,
    //    bool showSerialNumber, bool showBorder, float heightInPoints, string fontName, short fontSize)
    //{
    //    int serialNumber = 1;
    //    int columnStartIndex = 0;
    //    IFont font = workbook.CreateFont();
    //    font.FontName = fontName;
    //    font.FontHeightInPoints = fontSize;
    //    ICellStyle style = CreateCellStyle(workbook, columns[1], showBorder);
    //    style.SetFont(font);
    //    foreach (var item in items)
    //    {
    //        var row = sheet.CreateRow(currentRow++);
    //        row.HeightInPoints = heightInPoints;
    //        if (showSerialNumber)
    //        {
    //            var cell = row.CreateCell(0);
    //            cell.CellStyle = style;
    //            cell.SetCellValue(serialNumber);
    //            columnStartIndex = 1;
    //        }
    //        for (int columnIndex = columnStartIndex; columnIndex < columns.Count; columnIndex++)
    //        {
    //            var cell = row.CreateCell(columnIndex);
    //            Column column = columns[columnIndex];
    //            cell.CellStyle = style;
    //            CellSetValue(cell, column.Field, item);
    //        }
    //        serialNumber++;
    //    }
    //}

    //private static void BuildItems<T>(IWorkbook workbook, ISheet sheet, List<Column> columns,
    //    ref int currentRow, IList<T> items,
    //    string fontName, short fontSize, bool showSerialNumber)
    //{
    //    int serialNumber = 1;
    //    int columnStartIndex = 0;
    //    IFont font = workbook.CreateFont();
    //    font.FontName = fontName;
    //    font.FontHeightInPoints = fontSize;
    //    ICellStyle style = null;
    //    foreach (var item in items)
    //    {
    //        int columCount = columns.Count;
    //        var row = sheet.CreateRow(currentRow++);
    //        if (showSerialNumber)
    //        {
    //            var cell = row.CreateCell(0);
    //            style = CreateCellStyle(workbook, columns[0], false);
    //            style.SetFont(font);
    //            cell.CellStyle = style;
    //            cell.SetCellValue(serialNumber);
    //            columnStartIndex = 1;
    //            columCount++;
    //        }
    //        for (int columnIndex = columnStartIndex; columnIndex < columCount; columnIndex++)
    //        {
    //            var cell = row.CreateCell(columnIndex);

    //            Column column;
    //            if (showSerialNumber)
    //                column = columns[columnIndex - 1];
    //            else
    //            {
    //                if (columnIndex == 0)
    //                    style = CreateCellStyle(workbook, columns[0], false);

    //                column = columns[columnIndex];
    //            }

    //            style.SetFont(font);
    //            cell.CellStyle = style;
    //            CellSetValue(cell, column.Field, item);
    //        }
    //        serialNumber++;
    //    }
    //}

    //private static void CellSetValue<T>(ICell cell, string column, T item)
    //{
    //    var prop = item.GetType().GetProperty(column);

    //    if (prop != null)
    //    {
    //        string value = "";
    //        if (prop.GetValue(item, null) != null)
    //            value = prop.GetValue(item, null).ToString().Replace("\n", "").Trim();
    //        switch (prop.PropertyType.FullName)
    //        {
    //            case "System.String"://字符串类型
    //                cell.SetCellValue(value);
    //                break;
    //            case "System.DateTime"://日期类型
    //                DateTime dateV;
    //                DateTime.TryParse(value, out dateV);
    //                if (dateV == DateTime.MinValue)
    //                    cell.SetCellValue("");
    //                else
    //                    cell.SetCellValue(dateV.ToShortDateString());
    //                break;
    //            case "System.Boolean"://布尔型
    //                bool boolV = false;
    //                bool.TryParse(value, out boolV);
    //                cell.SetCellValue(boolV);
    //                break;
    //            case "System.Int16"://整型
    //            case "System.Int32":
    //            case "System.Int64":
    //            case "System.Byte":
    //                int intV = 0;
    //                int.TryParse(value, out intV);
    //                cell.SetCellValue(intV);
    //                break;
    //            case "System.Decimal"://浮点型
    //            case "System.Double":
    //                double doubV = 0;
    //                double.TryParse(value, out doubV);
    //                cell.SetCellValue(doubV);
    //                break;
    //            case "System.DBNull"://空值处理
    //                cell.SetCellValue("");
    //                break;
    //            default:
    //                string n = prop.PropertyType.FullName;
    //                cell.SetCellValue(value);
    //                break;
    //        }
    //    }
    //}

    ///// <summary>
    ///// 复制文件
    ///// </summary>
    ///// <param name="sourceFileName"></param>
    ///// <param name="destFileName"></param>
    ///// <returns></returns>
    //public static string CopyFile(string sourceFileName, out string destFileName)
    //{
    //    destFileName = "";
    //    lock (_lockObject)
    //    {
    //        string templatePath = "";
    //        if (!Path.IsPathRooted(sourceFileName))
    //        {
    //            templatePath = Path.Combine(_basePath, TEMPLATE_FOLDER, sourceFileName);
    //        }
    //        if (File.Exists(templatePath))
    //        {
    //            string outputFileFolder = Path.Combine(_basePath, TEMPLATE_FOLDER, OUTPUT_FOLDER);
    //            if (!Directory.Exists(outputFileFolder))
    //                Directory.CreateDirectory(outputFileFolder);

    //            int index = sourceFileName.IndexOf("_", 0);
    //            if (index > 0)
    //                sourceFileName = sourceFileName.Substring(index + 1, sourceFileName.Length - index - 1);

    //            destFileName = sourceFileName.TrimEnd('.', 'd', 'o', 'c', 'x') + "_" + Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHH") + ".docx";

    //            string outputFilePath = System.IO.Path.Combine(outputFileFolder, destFileName);
    //            File.Copy(templatePath, outputFilePath);

    //            return outputFilePath;
    //        }

    //        return string.Empty;
    //    }
    //}
    //#region 客户端使用导出
    ///// <summary>
    ///// 客户端使用
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="datas"></param>
    ///// <param name="title"></param>
    ///// <param name="fileName"></param>
    ///// <param name="savePath"></param>
    ///// <param name="showSerialNumber"></param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <returns></returns>
    //public static bool ExportforClient<T>(List<List<T>> datas, string title, string fileName, string savePath, bool showSerialNumber = false, string fontName = "宋体", short fontSize = 12)
    //{
    //    bool rslt = true;
    //    try
    //    {
    //        IWorkbook workbook = BuildforClient<T>(title, datas, fontName, fontSize, showSerialNumber);
    //        MemoryStream stream = new MemoryStream();
    //        workbook.Write(stream);
    //        byte[] fileBytes = stream.ToArray();
    //        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
    //        FileStream fs = new FileStream(Path.Combine(string.Format(@"{0}\{1}.xls", savePath, fileName)), FileMode.Create, FileAccess.Write);
    //        fs.Write(fileBytes, 0, fileBytes.Length);
    //        fs.Flush();
    //        fs.Close();
    //    }
    //    catch
    //    {

    //        rslt = false;
    //    }
    //    return rslt;

    //}
    ///// <summary>
    ///// 客户端创建工作簿
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="title"></param>
    ///// <param name="items"></param>
    ///// <param name="fontName"></param>
    ///// <param name="fontSize"></param>
    ///// <param name="showSerialNumber"></param>
    ///// <param name="haveHeader"></param>
    ///// <returns></returns>
    //public static IWorkbook BuildforClient<T>(string title, List<List<T>> items, string fontName, short fontSize, bool showSerialNumber, bool haveHeader = true)
    //{
    //    HSSFWorkbook workbook = new HSSFWorkbook();
    //    ISheet sheet = workbook.CreateSheet(title);

    //    int currentRow = 0;

    //    #region excell文件附属信息

    //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
    //    dsi.Company = COMPANY_NAME;
    //    workbook.DocumentSummaryInformation = dsi;

    //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
    //    si.Author = COMPANY_NAME; //填加xls文件作者信息
    //    si.ApplicationName = COMPANY_NAME; //填加xls文件创建程序信息
    //    si.LastAuthor = COMPANY_NAME; //填加xls文件最后保存者信息
    //    si.Comments = COMPANY_NAME; //填加xls文件作者信息
    //    si.Title = COMPANY_NAME; //填加xls文件标题信息
    //    si.Subject = COMPANY_NAME;//填加文件主题信息
    //    si.CreateDateTime = DateTime.Now;
    //    workbook.SummaryInformation = si;

    //    #endregion

    //    #region excell文件表头

    //    if (haveHeader)
    //    {
    //        int columnsCount = items[0].Count;
    //        if (showSerialNumber)
    //            columnsCount++;
    //        BuildHeader(workbook, sheet, title, ref currentRow, columnsCount);
    //    }

    //    #endregion

    //    #region excell文件列头

    //    BuildColumnHeaderforClient(workbook, sheet, ref currentRow, items[0], showSerialNumber, fontName, fontSize);

    //    #endregion

    //    #region excell文件行

    //    BuildItemsforClient(workbook, sheet, items, ref currentRow, fontName, fontSize, showSerialNumber);

    //    #endregion

    //    return workbook;
    //}
    //private static void BuildColumnHeaderforClient<T>(IWorkbook workbook, ISheet sheet, ref int currentRow, List<T> headColumns, bool showSerialNumber, string fontName, short fontSize)
    //{
    //    IRow row = sheet.CreateRow(currentRow++);
    //    ICellStyle style = workbook.CreateCellStyle();
    //    style.Alignment = HorizontalAlignment.Center;
    //    IFont font = workbook.CreateFont();
    //    font.FontHeightInPoints = fontSize;
    //    font.FontName = fontName;
    //    font.Boldweight = 700;
    //    style.SetFont(font);
    //    int columnStartIndex = 0;
    //    if (showSerialNumber)
    //    {
    //        var cell = row.CreateCell(0);
    //        cell.CellStyle = style;
    //        cell.SetCellValue("序号");
    //        sheet.SetColumnWidth(columnStartIndex, 30 * 40);
    //    }
    //    for (int columnIndex = 0; columnIndex < headColumns.Count; columnIndex++)
    //    {
    //        ICell cell = null;
    //        if (showSerialNumber)
    //        {
    //            cell = row.CreateCell(columnIndex + 1);
    //            sheet.SetColumnWidth(columnIndex + 1, 100 * 40);
    //        }
    //        else
    //        {
    //            cell = row.CreateCell(columnIndex);
    //            sheet.SetColumnWidth(columnIndex, 100 * 40);
    //        }
    //        cell.CellStyle = style;
    //        T feild = headColumns[columnIndex];
    //        CellSetValue<T>(cell, "FieldName", feild);


    //    }
    //}
    //private static void BuildItemsforClient<T>(IWorkbook workbook, ISheet sheet, List<List<T>> items,
    //   ref int currentRow,
    //   string fontName, short fontSize, bool showSerialNumber)
    //{
    //    int serialNumber = 1;
    //    IFont font = workbook.CreateFont();
    //    font.FontName = fontName;
    //    font.FontHeightInPoints = fontSize;
    //    ICellStyle style = null;
    //    foreach (List<T> item in items)
    //    {
    //        int columCount = item.Count;
    //        var row = sheet.CreateRow(currentRow++);
    //        if (showSerialNumber)
    //        {
    //            var cell = row.CreateCell(0);
    //            style = CreateCellStyle(workbook, new Column() { HValue = HorizontalAlignment.Center, VValue = VerticalAlignment.Center }, false);
    //            style.SetFont(font);
    //            cell.CellStyle = style;
    //            cell.SetCellValue(serialNumber);
    //        }
    //        for (int columnIndex = 0; columnIndex < columCount; columnIndex++)
    //        {
    //            ICell cell = null;
    //            if (showSerialNumber)
    //                cell = row.CreateCell(columnIndex + 1);
    //            else
    //                cell = row.CreateCell(columnIndex);
    //            if (columnIndex == 0)
    //            {
    //                style = CreateCellStyle(workbook, new Column() { HValue = HorizontalAlignment.Left, VValue = VerticalAlignment.Center }, false);
    //            }
    //            T column = item[columnIndex];
    //            style.SetFont(font);
    //            cell.CellStyle = style;
    //            CellSetValue(cell, "FieldValue", column);
    //        }
    //        serialNumber++;
    //    }
    //}

    //#endregion

    #endregion
}

/// <summary>
/// excel列
/// </summary>
public class Column
{
    /// <summary>
    /// 索引（若有值表示列标题固定）
    /// </summary>
    public int? Index { get; set; }

    /// <summary>
    /// 列序号
    /// </summary>
    public string No { get; set; }

    /// <summary>
    /// 宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 字段
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 水平对齐
    /// </summary>
    public HorizontalAlignment HValue { get; set; }

    /// <summary>
    /// 垂直对齐
    /// </summary>
    public VerticalAlignment VValue { get; set; }

    /// <summary>
    /// 必须
    /// </summary>
    public bool Required { get; set; }
}

/// <summary>
/// excel单元格
/// </summary>
public class MyCell
{
    /// <summary>
    /// 行索引
    /// </summary>
    public int RowIndex { get; set; }

    /// <summary>
    /// 列索引
    /// </summary>
    public int ColIndex { get; set; }

    /// <summary>
    /// 单元格值
    /// </summary>
    public string Val { get; set; }
}