﻿namespace Dry.EF.Sqlite;

/// <summary>
/// sqlite ef上下文工厂（生产迁移脚本用）
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class SqliteDbContextFactory<TBoundedContext> : DbContextFactory<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    protected override string ConnectionString => "Data Source=Default.db";
}