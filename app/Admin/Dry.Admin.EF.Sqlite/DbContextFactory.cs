using Dry.Admin.Domain;
using Dry.Admin.EF.Contexts;
using Dry.Admin.EF.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.EF.Sqlite
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
    {
        public AdminDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=Default.db";
            var serviceProvider = new ServiceCollection()
                .AddAdminSqliteEFContext(connectionString)
                .AddAdminEF()
                .BuildServiceProvider();
            return serviceProvider.GetService<IAdminContext>() as AdminDbContext;
        }
    }
}