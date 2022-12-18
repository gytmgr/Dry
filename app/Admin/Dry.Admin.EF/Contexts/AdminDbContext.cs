global using Dry.Admin.Domain;
global using Dry.EF.Contexts;
global using Dry.EF.EntityConfigs;
global using Microsoft.EntityFrameworkCore;
global using System.Collections.Generic;
global using Dry.Admin.Domain.Entities;
global using Dry.Admin.Domain.Shared.Enums;
global using Dry.Core.Utilities;
global using Dry.Dependency;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Dry.Domain.Entities.ValueObjects;
global using System.Linq.Expressions;

namespace Dry.Admin.EF.Contexts;

/// <summary>
/// ef上下文
/// </summary>
public class AdminDbContext : DryDbContext<IAdminContext>, IAdminContext
{
    public AdminDbContext(DbContextOptions options, IEnumerable<IEntityRegister<IAdminContext>> entityRegisters)
         : base(options, entityRegisters)
    {
    }
}