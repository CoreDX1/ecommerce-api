using Application.DTOs.Request.User;
using FluentValidation;

namespace Application.Validations;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDTO>
{
    public RegisterUserValidator() { }
}
