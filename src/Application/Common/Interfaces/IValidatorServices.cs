using Ardalis.Result;
using FluentValidation.Results;

namespace Application.Common.Interfaces;

public interface IValidatorServices
{
    List<ValidationError> GetValidationError(ValidationResult validationResult);
    Result<T> GetInvalidResult<T>(ValidationResult validationResult);
    Result GetInvalidResult(ValidationResult validationResult);
}
