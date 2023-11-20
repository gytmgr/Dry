namespace Dry.EF.Extensions;

/// <summary>
/// 数据迁移扩展
/// </summary>
public static class MigrationBuilderExtension
{
    /// <summary>
    /// 添加或更新表说明
    /// </summary>
    /// <param name="migrationBuilder">迁移构造器</param>
    /// <param name="tableName">表名</param>
    /// <param name="description">说明</param>
    /// <param name="schema">架构</param>
    public static void AddOrUpdateTableDescription(this MigrationBuilder migrationBuilder, string tableName,
        string description, string schema = "dbo")
    {
        if (!string.IsNullOrWhiteSpace(description) && description.Contains('\'')) description = description.Replace("'", "''");
        migrationBuilder.Sql(MigrationSqlTemplate.AddTableDbDescriptionTemplate
            .Replace("{tableDescription}", description)
            .Replace("{schema}", schema)
            .Replace("{tableName}", tableName));
    }

    /// <summary>
    /// 添加或更新列说明
    /// </summary>
    /// <param name="migrationBuilder">迁移构造器</param>
    /// <param name="tableName">表名</param>
    /// <param name="columnName">列名</param>
    /// <param name="description">说明</param>
    /// <param name="schema">架构</param>
    public static void AddOrUpdateColumnDescription(this MigrationBuilder migrationBuilder, string tableName,
        string columnName, string description, string schema = "dbo")
    {
        if (!string.IsNullOrWhiteSpace(description) && description.Contains('\'')) description = description.Replace("'", "''");
        migrationBuilder.Sql(MigrationSqlTemplate.AddColumnDbDescriptionTemplate
            .Replace("{columnDescription}", description)
            .Replace("{schema}", schema)
            .Replace("{tableName}", tableName)
            .Replace("{columnName}", columnName));
    }

    /// <summary>
    /// 从模型注解添加表和列说明，需要先在OnModelCreating方法调用ConfigDatabaseDescription生成注解
    /// </summary>
    /// <param name="migrationBuilder"></param>
    /// <param name="migration"></param>
    /// <returns></returns>
    public static MigrationBuilder ApplyDatabaseDescription(this MigrationBuilder migrationBuilder, Migration migration)
    {
        var defaultSchema = "dbo";
        var descriptionAnnotationName = ModelBuilderExtension.DbDescriptionAnnotationName;

        foreach (var entityType in migration.TargetModel.GetEntityTypes())
        {
            //添加表说明
            var tableName = entityType.GetTableName();
            var schema = entityType.GetSchema();
            var tableDescriptionAnnotation = entityType.FindAnnotation(descriptionAnnotationName);

            if (tableDescriptionAnnotation != null)
            {
                migrationBuilder.AddOrUpdateTableDescription(
                    tableName!,
                    tableDescriptionAnnotation.Value!.ToString()!,
                    string.IsNullOrEmpty(schema) ? defaultSchema : schema);
            }

            //添加列说明
            foreach (var property in entityType.GetProperties())
            {
                var columnDescriptionAnnotation = property.FindAnnotation(descriptionAnnotationName);

                if (columnDescriptionAnnotation != null)
                {
                    migrationBuilder.AddOrUpdateColumnDescription(
                        tableName!,
                        property.GetColumnBaseName(),
                        columnDescriptionAnnotation.Value!.ToString()!,
                        string.IsNullOrEmpty(schema) ? defaultSchema : schema);
                }
            }
        }

        return migrationBuilder;
    }
}

/// <summary>
/// 数据迁移扩展Sql模板
/// </summary>
public static class MigrationSqlTemplate
{
    /// <summary>
    /// 添加表说明
    /// </summary>
    public const string AddTableDbDescriptionTemplate = @"
            if exists (
	            select t.name as tname, d.value as Description
	            from sysobjects t
	            left join sys.extended_properties d
	            on t.id = d.major_id and d.minor_id = 0 and d.name = 'MS_Description'
	            where t.name = '{tableName}' and d.value is not null)
            begin
	            exec sys.sp_dropextendedproperty
                @name=N'MS_Description'
              , @level0type=N'SCHEMA'
              , @level0name=N'{schema}'
              , @level1type=N'TABLE'
              , @level1name=N'{tableName}'
              , @level2type=NULL
              , @level2name=NULL
            end
            go

            exec sys.sp_addextendedproperty
                @name=N'MS_Description'
              , @value=N'{tableDescription}'
              , @level0type=N'SCHEMA'
              , @level0name=N'{schema}'
              , @level1type=N'TABLE'
              , @level1name=N'{tableName}'
              , @level2type= NULL
              , @level2name= NULL
            go";

    /// <summary>
    /// 添加列说明
    /// </summary>
    public const string AddColumnDbDescriptionTemplate = @"
            if exists (
	            select t.name as tname,c.name as cname, d.value as Description
	            from sysobjects t
	            left join syscolumns c
	            on c.id=t.id and t.xtype='U' and t.name<>'dtproperties'
	            left join sys.extended_properties d
	            on c.id=d.major_id and c.colid=d.minor_id and d.name = 'MS_Description'
	            where t.name = '{tableName}' and c.name = '{columnName}' and d.value is not null)
            begin
	            exec sys.sp_dropextendedproperty
                @name=N'MS_Description'
              , @level0type=N'SCHEMA'
              , @level0name=N'{schema}'
              , @level1type=N'TABLE'
              , @level1name=N'{tableName}'
              , @level2type=N'COLUMN'
              , @level2name=N'{columnName}'
            end
            go

            exec sys.sp_addextendedproperty
                @name=N'MS_Description'
              , @value=N'{columnDescription}'
              , @level0type=N'SCHEMA'
              , @level0name=N'{schema}'
              , @level1type=N'TABLE'
              , @level1name=N'{tableName}'
              , @level2type=N'COLUMN'
              , @level2name=N'{columnName}'
            go";
}