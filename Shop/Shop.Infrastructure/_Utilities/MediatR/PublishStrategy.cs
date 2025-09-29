namespace Shop.Infrastructure._Utilities.MediatR;

public enum PublishStrategy
{
    /// <summary>
    /// Run each handler sequentially; continue on exceptions, aggregate at the end.
    /// </summary>
    SyncContinueOnException = 0,

    /// <summary>
    /// Run each handler sequentially; stop at first exception.
    /// </summary>
    SyncStopOnException = 1,

    /// <summary>
    /// Run handlers asynchronously (await all); aggregate exceptions.
    /// </summary>
    Async = 2,

    /// <summary>
    /// Fire-and-forget: schedule each handler on ThreadPool and return immediately.
    /// </summary>
    ParallelNoWait = 3,

    /// <summary>
    /// Run handlers on ThreadPool and await Task.WhenAll.
    /// </summary>
    ParallelWhenAll = 4,

    /// <summary>
    /// Run handlers on ThreadPool and await Task.WhenAny.
    /// </summary>
    ParallelWhenAny = 5,
}