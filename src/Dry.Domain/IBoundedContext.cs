global using Dry.Core.Model;
global using Dry.Dependency;
global using Dry.Domain.Entities;
global using Dry.Domain.Queryables;
global using Dry.Domain.Repositories;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using System.Diagnostics.CodeAnalysis;
global using System.Linq.Expressions;

namespace Dry.Domain;

/// <summary>
/// 限界上下文
/// </summary>
public interface IBoundedContext
{
}