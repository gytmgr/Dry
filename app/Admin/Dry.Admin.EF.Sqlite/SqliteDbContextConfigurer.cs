global using Dry.Admin.Domain;
global using Dry.EF.Contexts;
global using Dry.EF.Sqlite;
global using Microsoft.EntityFrameworkCore;

namespace Dry.Admin.EF.Sqlite;

public class SqliteDbContextConfigurer : SqliteDbContextConfigurer<IAdminContext>
{
}