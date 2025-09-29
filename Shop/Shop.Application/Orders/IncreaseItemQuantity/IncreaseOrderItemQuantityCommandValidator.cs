using FluentValidation;

namespace Shop.Application.Orders.IncreaseItemQuantity
{
    public class IncreaseOrderItemQuantityCommandValidator:AbstractValidator<IncreaseOrderItemQuantityCommand>
    {
        public IncreaseOrderItemQuantityCommandValidator()
        {
            RuleFor(f => f.Count)
                .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
        }
    }
}
