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
    public TCode? Code { get; set; }

    /// <summary>
    /// 返回信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Result<TCode> Create(TCode code)
        => Create(code, null);

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Result<TCode> Create(string message)
        => Create(default, message);

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Result<TCode> Create(TCode? code, string? message)
        => new Result<TCode>
        {
            Code = code,
            Message = message
        };
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
    public TData? Data { get; set; }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TData data)
        => Create(default, default, data);

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TCode code, TData? data = default)
        => Create(code, default, data);

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(string message, TData? data = default)
        => Create(default, message, data);

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result<TCode, TData> Create(TCode? code, string? message, TData? data = default)
        => new Result<TCode, TData>
        {
            Code = code,
            Message = message,
            Data = data
        };
}