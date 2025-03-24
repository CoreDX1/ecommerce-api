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
    private readonly IValidator<UpdateProductRequestDTO> _updateProductValidator;
    private readonly IValidatorServices _validatorServices;

    public ProductServices(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CreateProductRequestDTO> createProductValidator,
        IValidatorServices validatorServices,
        IValidator<UpdateProductRequestDTO> updateProductValidator
    )
        : base(unitOfWork, mapper)
    {
        _createProductValidator = createProductValidator;
        _validatorServices = validatorServices;
        _updateProductValidator = updateProductValidator;
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

    // Esto tiene que estar en el Servico generico
    // Que los nombres de los productos no se repitan
    public async Task<Result<ProductResponseDTO>> CreateProduct(CreateProductRequestDTO createProduct)
    {
        var validationResult = await _createProductValidator.ValidateAsync(createProduct);

        if (!validationResult.IsValid)
            return Result.Invalid(_validatorServices.GetValidationError(validationResult));

        await AddAsync(createProduct);

        var productCreated = _unitOfWork.ProductRepository.GetProductByName(createProduct.Name);

        var productResponse = _mapper.Map<ProductResponseDTO>(productCreated);

        return Result.Success(productResponse, ReplyMessage.Success.Save);
    }

    // Que los nombres de los productos no se repitan
    public async Task<Result<ProductResponseDTO>> UpdateProduct(UpdateProductRequestDTO updateProduct)
    {
        var validationResult = await _updateProductValidator.ValidateAsync(updateProduct);
        if (!validationResult.IsValid)
            return Result.Invalid(_validatorServices.GetValidationError(validationResult));

        await UpdateAsync(updateProduct);

        Product? productUpdated = await _unitOfWork.ProductRepository.FindOneAsync(x => x.Name.ToLower() == updateProduct.Name.ToLower());

        var productResponse = _mapper.Map<ProductResponseDTO>(productUpdated);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public async Task<Result<ProductResponseDTO>> DeleteProduct(int productId)
    {
        await DeleteAsync(productId);

        var productDeleted = await _unitOfWork.ProductRepository.FindOneAsync(x => x.ProductId == productId);
        if (productDeleted == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<ProductResponseDTO>(productDeleted);

        return Result.Success(productResponse, ReplyMessage.Success.Delete);
    }

    public async Task<Result<ProductResponseDTO>> GetProductById(int productId)
    {
        Product? product = await _unitOfWork.ProductRepository.FindOneAsync(x => x.ProductId == productId);

        if (product == null)
            return Result.NotFound(ReplyMessage.Error.NotFound);

        var productResponse = _mapper.Map<ProductResponseDTO>(product);

        return Result.Success(productResponse, ReplyMessage.Success.Query);
    }

    public Task<Result<ProductResponseDTO>> GetProductByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }
}
