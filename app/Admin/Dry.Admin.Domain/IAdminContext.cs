global using Dry.Admin.Domain.Entities;
global using Dry.Admin.Domain.Events;
global using Dry.Admin.Domain.Shared.Enums;
global using Dry.Core.Model;
global using Dry.Domain.Entities;
global using Dry.Domain.Entities.ValueObjects;
global using Dry.Domain.Extensions;
global using Dry.Domain.Shared;
global using System.Diagnostics.CodeAnalysis;

namespace Dry.Admin.Domain;

/// <summary>
/// 鉴权中心界限上下文接口
/// </summary>
public interface IAdminContext : IBoundedContext
{
}