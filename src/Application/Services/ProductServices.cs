using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.DTOs.Request.Product;
using Application.DTOs.Response.Product;
using Ardalis.Result;
using AutoMapper;
using Domain.Common.Constants;
using Domain.Entity;
using FluentValidation;

namespace Application.Services;

public class ProductServices : GenericServiceAsync<Product, ProductResponseDTO>, IProductServices
{
    private readonly IValidator<CreateProductRequestDTO> _createProductValidator;
    private readonly IValidatorServices _validatorServices;

    public ProductServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateProductRequestDTO> createProductValidator,
        IValidatorServices validatorServices
    )
        : base(unitOfWork, mapper)
    {
        _createProductValidator = createProductValidator;
        _validatorServices = validatorServices;
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductByName(string name)
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetProductByName(name);

        if (products == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetByPaginationAsync(int page, int recordsPerPage)
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetByPaginationAsync(page, recordsPerPage);

        if (products == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetProductsByFilter(FilterProductRequestDTO filter)
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetProductsByFilter(filter);

        if (products == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public async Task<Result<IEnumerable<ProductResponseDTO>>> GetAllProducts()
    {
        IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetAllProducts();

        if (products == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<IEnumerable<ProductResponseDTO>>(products);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public async Task<Result<ProductResponseDTO>> CreateProduct(CreateProductRequestDTO createProduct)
    {
        var validationResult = await _createProductValidator.ValidateAsync(createProduct);

        if (!validationResult.IsValid)
            return Result.Invalid(_validatorServices.GetValidationError(validationResult));

        var productMapper = _mapper.Map<Product>(createProduct);

        await AddAsync(productMapper);

        var productCreated = _unitOfWork.ProductRepository.GetByIdAsync(productMapper.ProductId);

        var productResponse = _mapper.Map<ProductResponseDTO>(productCreated);

        return Result.Success(productResponse, ReplyMessage.Success.Save);
    }

    public Task<Result<ProductResponseDTO>> UpdateProduct(UpdateProductRequestDTO updateProduct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductResponseDTO>> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductResponseDTO>> GetProductById(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProductResponseDTO>> GetProductByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }
}
