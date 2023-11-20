namespace Dry.EF.Extensions;

/// <summary>
/// ef模型创建扩展
/// </summary>
public static class ModelBuilderExtension
{
    /// <summary>
    /// 数据库说明
    /// </summary>
    public const string DbDescriptionAnnotationName = "DbDescription";

    /// <summary>
    /// 配置数据库表和列说明
    /// </summary>
    /// <param name="modelBuilder">模型构造器</param>
    /// <returns>模型构造器</returns>
    public static ModelBuilder ConfigDatabaseDescription(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //添加表说明
            if (entityType.FindAnnotation(DbDescriptionAnnotationName) == null &&
                entityType.ClrType?.CustomAttributes.Any(attr => attr.AttributeType == typeof(DescriptionAttribute)) == true)
            {
                entityType.AddAnnotation(DbDescriptionAnnotationName,
                    (entityType.ClrType.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description);
            }

            //添加列说明
            foreach (var property in entityType.GetProperties())
            {
                if (property.FindAnnotation(DbDescriptionAnnotationName) == null &&
                    property.PropertyInfo?.CustomAttributes.Any(attr => attr.AttributeType == typeof(DescriptionAttribute)) == true)
                {
                    var propertyInfo = property.PropertyInfo;
                    var propertyType = propertyInfo.PropertyType;
                    //如果该列的实体属性是枚举类型，把枚举的说明追加到列说明
                    var enumDbDescription = string.Empty;
                    if (propertyType.IsEnum || (propertyType.IsDerivedFrom(typeof(Nullable<>)) && propertyType.GenericTypeArguments[0].IsEnum))
                    {
                        var @enum = propertyType.IsDerivedFrom(typeof(Nullable<>))
                            ? propertyType.GenericTypeArguments[0]
                            : propertyType;

                        var descList = new List<string>();
                        foreach (var field in @enum?.GetFields() ?? new FieldInfo[0])
                        {
                            if (!field.IsSpecialName)
                            {
                                var desc = (field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                    .FirstOrDefault() as DescriptionAttribute)?.Description;
                                descList.Add(
                                    $@"{field.GetRawConstantValue()} : {(string.IsNullOrWhiteSpace(desc) ? field.Name : desc)}");
                            }
                        }

                        var isFlags = @enum?.GetCustomAttribute(typeof(FlagsAttribute)) != null;
                        var enumTypeDbDescription =
                            (@enum?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                                DescriptionAttribute)?.Description;
                        enumTypeDbDescription += enumDbDescription + (isFlags ? " [是标志位枚举]" : string.Empty);
                        enumDbDescription =
                            $@"( {(string.IsNullOrWhiteSpace(enumTypeDbDescription) ? "" : $@"{enumTypeDbDescription}; ")}{string.Join("; ", descList)} )";
                    }

                    property.AddAnnotation(DbDescriptionAnnotationName,
                        $@"{(propertyInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)
                            ?.Description}{(string.IsNullOrWhiteSpace(enumDbDescription) ? "" : $@" {enumDbDescription}")}");
                }
            }
        }

        return modelBuilder;
    }
}