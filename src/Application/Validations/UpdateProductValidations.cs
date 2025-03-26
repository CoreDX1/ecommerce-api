using Application.DTOs.Request.Product;
using FluentValidation;

namespace Application.Validations;

public class UpdateProductValidations : AbstractValidator<UpdateProductRequestDTO>
{
    public UpdateProductValidations()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
