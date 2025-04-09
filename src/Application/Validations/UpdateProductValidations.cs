using Application.DTOs.Request.Product;
using FluentValidation;

namespace Application.Validations;

public class UpdateProductValidations : AbstractValidator<UpdateProductRequestDTO>
{
    public UpdateProductValidations()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Category ID is required for update");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required for update");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required for update");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than 0");
    }
}
