using Application.Common.Interfaces;
using Ardalis.Result;
using FluentValidation.Results;

namespace Application.Services;

public class ValidatorServices : IValidatorServices
{
    public List<ValidationError> GetValidationError(ValidationResult validationResult)
    {
        return validationResult
            .Errors.Select(error => new ValidationError
            {
                ErrorMessage = error.ErrorMessage,
                Identifier = error.PropertyName,
                ErrorCode = error.ErrorCode,
            })
            .ToList();
    }

    // Opcional: Ayuda a devolver directamente un Result.Invalid
    public Result<T> GetInvalidResult<T>(ValidationResult validationResult)
    {
        return Result<T>.Invalid(GetValidationError(validationResult));
    }

    public Result GetInvalidResult(ValidationResult validationResult)
    {
        return Result.Invalid(GetValidationError(validationResult));
    }
}
