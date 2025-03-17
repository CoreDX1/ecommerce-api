using System.Linq.Expressions;
using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Interfaces.Persistence;

namespace Application.Services;

public class ReadServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public ReadServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TDto>>> GetAllAsync()
    {
        List<TEntity> entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();

        var entitiesResponse = _mapper.Map<IEnumerable<TDto>>(entities);

        if (entities == null)
            return Result.NotFound("Entities not found");

        return Result.Success(entitiesResponse, "Entities retrieved successfully");
    }

    public async Task<Result<TDto>> GetByIdAsync(int id)
    {
        TEntity entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);

        if (entity == null)
            return Result.NotFound("Entity not found");

        var entityResponse = _mapper.Map<TDto>(entity);

        return Result.Success(entityResponse, "Entity retrieved successfully");
    }

    public async Task<Result<IEnumerable<TDto>>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();
        var entitiesResponse = _mapper.Map<IEnumerable<TDto>>(entities);

        if (entities == null)
            return Result.NotFound("Entities not found");

        return Result.Success(entitiesResponse, "Entities retrieved successfully");
    }
}
