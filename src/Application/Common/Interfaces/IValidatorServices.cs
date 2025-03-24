using Ardalis.Result;
using FluentValidation.Results;

namespace Application.Common.Interfaces;

public interface IValidatorServices
{
    public List<ValidationError> GetValidationError(ValidationResult validationResult);
}
