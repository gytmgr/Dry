namespace Dry.EF.Repositories;

/// <summary>
/// 工作单元
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public class UnitOfWork<TBoundedContext> : IUnitOfWork<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// ef上下文
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// 中介者
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mediator"></param>
    public UnitOfWork(IDryDbContext<TBoundedContext> context, IMediator mediator)
    {
        _context = (context as DbContext)!;
        _mediator = mediator;
    }

    /// <summary>
    /// 异步提交
    /// </summary>
    /// <returns></returns>
    public virtual async Task<int> CompleteAsync()
    {
        var changeEntries = _context.ChangeTracker.Entries<IEvents>().Where(x => x.Entity.GetEvent().Any());
        var events = changeEntries.SelectMany(x => x.Entity.GetEvent()).ToArray();
        changeEntries.ToList().ForEach(entity => entity.Entity.ClearEvent());

        var saveExecuteEvents = events.Where(x => x.PreExecute).ToArray();
        foreach (var saveExecuteEvent in saveExecuteEvents)
        {
            await _mediator.Publish(saveExecuteEvent);
        }

        var result = await _context.SaveChangesAsync();

        var savedExecuteEvents = events.Where(x => !x.PreExecute).ToArray();
        foreach (var savedExecuteEvent in savedExecuteEvents)
        {
            await _mediator.Publish(savedExecuteEvent);
        }

        return result;
    }
}