namespace Dry.Core.Model;

/// <summary>
/// 通用返回结果
/// </summary>
/// <typeparam name="TCode"></typeparam>
public class Result<TCode>
{
    /// <summary>
    /// 返回代码
    /// </summary>
    public TCode Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Result<TCode> Create(TCode code)
    {
        return new Result<TCode>
        {
            Code = code
        };
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Result<TCode> Create(string message)
    {
        return new Result<TCode>
        {
            Message = message
        };
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Result<TCode> Create(TCode code, string message)
    {
        var result = Create(message);
        result.Code = code;
        return result;
    }
}

/// <summary>
/// 通用返回结果
/// </summary>
/// <typeparam name="TCode"></typeparam>
/// <typeparam name="TData"></typeparam>
public class Result<TCode, TData> : Result<TCode>
{
    /// <summary>
    /// 返回数据
    /// </summary>
    public TData Data { get; set; }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TData data)
    {
        return new Result<TCode, TData>
        {
            Data = data
        };
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TCode code, TData data)
    {
        var result = Create(data);
        result.Code = code;
        return result;
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(string message, TData data)
    {
        var result = Create(data);
        result.Message = message;
        return result;
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TCode code, string message, TData data)
    {
        var result = Create(message, data);
        result.Code = code;
        return result;
    }
}