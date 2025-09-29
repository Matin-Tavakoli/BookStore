using MediatR;
using Shop.Domain.Order.Eventes;

namespace Shop.Application.Orders._EventHandelers;

public class SendSmsOrderFinalizedEventHandler:INotificationHandler<OrderFinalized>
{
    public async Task Handle(OrderFinalized notification, CancellationToken cancellationToken)
    {
        await Task.Delay(10000, cancellationToken);
        Console.WriteLine("------------------------------------------------------------");
    }
}