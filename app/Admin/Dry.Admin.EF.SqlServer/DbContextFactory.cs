namespace Dry.Admin.EF.SqlServer;

public class DbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
{
    public AdminDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
        var serviceProvider = new ServiceCollection()
            .AddDependency()
            .AddAdminSqlServerEFContext(connectionString)
            .AddEF()
            .BuildServiceProvider();
        return serviceProvider.GetService<IAdminContext>() as AdminDbContext;
    }
}