using FluentValidation;

namespace Shop.Application.Orders.DecreaseItemQuantity;

public class DecreaseOrderItemQuantityCommandValidator
    : AbstractValidator<DecreaseOrderItemQuantityCommand>
{
    public DecreaseOrderItemQuantityCommandValidator()
    {
        RuleFor(f => f.Count)
            .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
    }
}