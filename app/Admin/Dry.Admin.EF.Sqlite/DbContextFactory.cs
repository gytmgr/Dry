namespace Dry.Admin.EF.Sqlite;

public class DbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
{
    public AdminDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Data Source=Default.db";
        var serviceProvider = new ServiceCollection()
            .AddDependency()
            .AddAdminSqliteEFContext(connectionString)
            .AddEF()
            .BuildServiceProvider();
        return serviceProvider.GetService<IAdminContext>() as AdminDbContext;
    }
}