﻿global using Dry.Admin.Domain;
global using Dry.EF.SqlServer;

namespace Dry.Admin.EF.SqlServer;

public class DbContextConfigurer : SqlServerDbContextConfigurerBase<IAdminContext>
{
}