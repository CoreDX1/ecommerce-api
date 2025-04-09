using Application.DTOs.Request.Order;
using FluentValidation;

namespace Application.Validations;

// TODO: Incompleto
public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequestDTO>
{
    public UpdateOrderStatusRequestValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order ID is required for update");
        RuleFor(x => x.NewStatus).NotEmpty().WithMessage("New status is required for update");
    }
}
