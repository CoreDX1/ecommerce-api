using Application.DTOs.Request.Order;
using FluentValidation;

namespace Application.Validations;

public class CreateOrderItemRequestDTO { }

// TODO: Incompleto
public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequestDTO>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required for create");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Order must contain at least one item");
    }
}
