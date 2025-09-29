using Common.Application;
using MediatR;

namespace Shop.Application.Orders.Finalized;

public record OrderFinalizedCommand(long OrderId) : IBaseCommand;