using MediatR;
using System.Collections.Concurrent;

namespace Shop.Infrastructure._Utilities.MediatR;

/// <summary>
/// در v12 به جای ارث‌بری از Mediator، منطق publish را با INotificationPublisher کنترل می‌کنیم.
/// همچنین برای سازگاری با کدهای شما، همان متدهای ICustomPublisher را هم پیاده‌سازی کرده‌ایم.
/// </summary>
public class CustomPublisher : INotificationPublisher, ICustomPublisher
{
    private static readonly AsyncLocal<PublishStrategy?> _ambientStrategy = new();
    private readonly IPublisher _publisher;

    /// <summary>استراتژی پیش‌فرض وقتی روی نوتیف یا زمینه (Ambient) چیزی ست نشده.</summary>
    public PublishStrategy DefaultStrategy { get; set; } = PublishStrategy.SyncContinueOnException;

    public CustomPublisher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    // ===== ICustomPublisher facade (برای استفادهٔ آسان در کدهای اپ) =====

    public Task Publish<TNotification>(TNotification notification)
        => Publish(notification, DefaultStrategy, default);

    public Task Publish<TNotification>(TNotification notification, PublishStrategy strategy)
        => Publish(notification, strategy, default);

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        => Publish(notification, DefaultStrategy, cancellationToken);

    public async Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken)
    {
        var prev = _ambientStrategy.Value;
        _ambientStrategy.Value = strategy;
        try
        {
            await _publisher.Publish(notification!, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _ambientStrategy.Value = prev;
        }
    }

    // ===== INotificationPublisher (نقطه‌ی ورود MediatR هنگام Publish) =====

    public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        var strategy = ResolveStrategy(notification);
        return strategy switch
        {
            PublishStrategy.Async => AsyncContinueOnException(handlerExecutors, notification, cancellationToken),
            PublishStrategy.ParallelNoWait => ParallelNoWait(handlerExecutors, notification, cancellationToken),
            PublishStrategy.ParallelWhenAll => ParallelWhenAll(handlerExecutors, notification, cancellationToken),
            PublishStrategy.ParallelWhenAny => ParallelWhenAny(handlerExecutors, notification, cancellationToken),
            PublishStrategy.SyncStopOnException => SyncStopOnException(handlerExecutors, notification, cancellationToken),
            PublishStrategy.SyncContinueOnException or _ => SyncContinueOnException(handlerExecutors, notification, cancellationToken),
        };
    }

    private PublishStrategy ResolveStrategy(INotification notification)
    {
        // 1) اگر در محیط (Ambient) ست شده باشد، همان را استفاده کن
        if (_ambientStrategy.Value.HasValue)
            return _ambientStrategy.Value.Value;

        // 2) اگر خود نوتیفیکیشن اینترفیس IHavePublishStrategy را پیاده کرده باشد، از آن بخوان
        if (notification is IHavePublishStrategy s)
            return s.Strategy;

        // 3) در غیر این صورت، پیش‌فرض
        return DefaultStrategy;
    }

    // ===== اجرای استراتژی‌ها روی امضاهای v12 (NotificationHandlerExecutor) =====

    private Task ParallelWhenAll(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
        => Task.WhenAll(handlers.Select(h => Task.Run(() => h.HandlerCallback(n, ct), ct)));

    private Task ParallelWhenAny(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
        => Task.WhenAny(handlers.Select(h => Task.Run(() => h.HandlerCallback(n, ct), ct)));

    private Task ParallelNoWait(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
    {
        foreach (var h in handlers)
            _ = Task.Run(() => h.HandlerCallback(n, ct), ct);
        return Task.CompletedTask;
    }

    private async Task AsyncContinueOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
    {
        var tasks = new List<Task>();
        var errors = new List<Exception>();

        foreach (var h in handlers)
        {
            try { tasks.Add(h.HandlerCallback(n, ct)); }
            catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
            { errors.Add(ex); }
        }

        try { await Task.WhenAll(tasks).ConfigureAwait(false); }
        catch (AggregateException ex) { errors.AddRange(ex.Flatten().InnerExceptions); }
        catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
        { errors.Add(ex); }

        if (errors.Any()) throw new AggregateException(errors);
    }

    private async Task SyncStopOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
    {
        foreach (var h in handlers)
            await h.HandlerCallback(n, ct).ConfigureAwait(false);
    }

    private async Task SyncContinueOnException(IEnumerable<NotificationHandlerExecutor> handlers, INotification n, CancellationToken ct)
    {
        var errors = new List<Exception>();
        foreach (var h in handlers)
        {
            try { await h.HandlerCallback(n, ct).ConfigureAwait(false); }
            catch (AggregateException ex) { errors.AddRange(ex.Flatten().InnerExceptions); }
            catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
            { errors.Add(ex); }
        }
        if (errors.Any()) throw new AggregateException(errors);
    }
}

/// <summary>
/// اگر بخواهی استراتژی را روی خود نوتیف ست کنی، نوتیفیکیشن‌ها می‌توانند این را پیاده کنند.
/// </summary>
public interface IHavePublishStrategy
{
    PublishStrategy Strategy { get; }
}
