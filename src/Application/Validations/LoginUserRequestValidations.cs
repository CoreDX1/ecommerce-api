using Application.DTOs.Request.User;
using FluentValidation;

namespace Application.Validations;

public class LoginUserRequestValidations : AbstractValidator<LoginUserRequestDTO>
{
    public LoginUserRequestValidations()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
