using Common.Domain;

namespace Shop.Domain.Order.Eventes;

public class OrderFinalized : BaseDomainEvent
{
    public OrderFinalized(long orderId)
    {
        OrderId = orderId;
    }

    public long OrderId { get; private set; }
}