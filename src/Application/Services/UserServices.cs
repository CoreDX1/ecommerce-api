using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.Configuration;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Common.Constants;
using Domain.Entity;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class UserServices : GenericServiceAsync<User, UserResponseDTO>, IUserService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IValidator<LoginUserRequestDTO> _validator;
    private readonly IValidator<RegisterUserRequestDTO> _createUserValidator;
    private readonly IValidatorServices _validatorServices;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<LoginUserRequestDTO> validator,
        IOptions<JwtConfig> jwtConfig,
        IValidatorServices validatorServices,
        IValidator<RegisterUserRequestDTO> createUserValidator,
        IJwtTokenGenerator jwtTokenGenerator
    )
        : base(unitOfWork, mapper)
    {
        _validator = validator;
        _jwtConfig = jwtConfig.Value;
        _validatorServices = validatorServices;
        _createUserValidator = createUserValidator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<UserResponseDTO>> RegisterAsync(RegisterUserRequestDTO registerRequest)
    {
        var validationResult = await _createUserValidator.ValidateAsync(registerRequest);

        if (!validationResult.IsValid)
            return Result.Invalid(_validatorServices.GetValidationError(validationResult));

        User newUser = await _unitOfWork.UserRepository.RegisterUser(registerRequest);

        if (newUser == null)
            return Result.NotFound(ReplyMessages.Error.CreateFailed);

        var userResponse = _mapper.Map<UserResponseDTO>(newUser);

        return Result.Created(userResponse, ReplyMessages.Success.Save);
    }

    public async Task<Result<UserResponseDTO>> LoginAsync(LoginUserRequestDTO loginRequest)
    {
        var validationResult = await _validator.ValidateAsync(loginRequest);

        if (!validationResult.IsValid)
            return _validatorServices.GetInvalidResult(validationResult);

        User authenticatedUser = await _unitOfWork.UserRepository.AuthenticateAsync(loginRequest);

        if (authenticatedUser == null)
            return Result.Unauthorized(ReplyMessages.Validate.ValidateError);

        var userResponse = _mapper.Map<UserResponseDTO>(authenticatedUser);

        userResponse.VerificationToken = await _jwtTokenGenerator.GenerateTokenAsync(authenticatedUser);

        return Result.Success(userResponse, ReplyMessages.Success.Query);
    }
}
