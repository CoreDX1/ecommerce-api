using Application.DTOs.Request.Product;
using FluentValidation;

namespace Application.Validations;

public class CreateProductValidations : AbstractValidator<CreateProductRequestDTO>
{
    public CreateProductValidations()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required for create");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required for create");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price is required for create");
    }
}
