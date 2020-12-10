using Dry.Admin.Domain;
using Dry.Admin.EF.Contexts;
using Dry.Admin.EF.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.EF.SqlServer
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
    {
        public AdminDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
            var serviceProvider = new ServiceCollection()
                .AddAdminSqlServerEFContext(connectionString)
                .AddAdminEF()
                .BuildServiceProvider();
            return serviceProvider.GetService<IAdminContext>() as AdminDbContext;
        }
    }
}