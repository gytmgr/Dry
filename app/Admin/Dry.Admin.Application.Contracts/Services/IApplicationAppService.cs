﻿namespace Dry.Admin.Application.Contracts.Services;

/// <summary>
/// 应用服务接口
/// </summary>
public interface IApplicationAppService : IApplicationQueryService<ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>
{
    /// <summary>
    /// 获取应用类型
    /// </summary>
    /// <returns></returns>
    Task<KeyValuePair<int, string>[]> TypeArrayAsync();
}