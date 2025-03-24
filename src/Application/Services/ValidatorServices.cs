using Application.Common.Interfaces;
using Ardalis.Result;
using FluentValidation.Results;

namespace Application.Services;

public class ValidatorServices : IValidatorServices
{
    public List<ValidationError> GetValidationError(ValidationResult validationResult)
    {
        List<ValidationError> validationError = [];

        foreach (var error in validationResult.Errors)
        {
            validationError.Add(
                new ValidationError()
                {
                    ErrorMessage = error.ErrorMessage,
                    Identifier = error.PropertyName,
                    ErrorCode = error.ErrorCode,
                }
            );
        }

        return validationError;
    }
}
