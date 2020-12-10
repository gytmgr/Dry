using Dry.Admin.Domain;
using Dry.EF.Contexts;
using Dry.EF.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dry.Admin.EF.Contexts
{
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
}