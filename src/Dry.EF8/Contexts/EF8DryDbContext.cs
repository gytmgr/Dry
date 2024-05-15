namespace Dry.EF8.Contexts;

/// <summary>
/// ef8上下文
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public class EF8DryDbContext<TBoundedContext> : DryDbContext<TBoundedContext> where TBoundedContext : IBoundedContext
{
#if NET8_0_OR_GREATER

    /// <summary>
    /// 自动事务是否启用
    /// </summary>
    public override bool AutoTransactionsEnabled
    {
        get => Database.AutoTransactionBehavior is not AutoTransactionBehavior.Never;
        set => Database.AutoTransactionBehavior = value ? AutoTransactionBehavior.WhenNeeded : AutoTransactionBehavior.Never;
    }

#endif

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public EF8DryDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}