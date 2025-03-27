using Application.DTOs.Request.User;
using FluentValidation;

namespace Application.Validations;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDTO>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.UserLastName).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        // RuleFor(x => x.PhoneNumber).NotEmpty(); // Uncomment if phone number is mandatory
    }
}
