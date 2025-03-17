using Application.Interfaces;
using Ardalis.Result;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services;

public class ReadServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public ReadServiceAsync(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TDto>>> GetAllAsync()
    {
        List<TEntity> entities = await _repository.GetAllAsync();

        var entitiesResponse = _mapper.Map<IEnumerable<TDto>>(entities);

        if (entities == null)
            return Result.NotFound("Entities not found");

        return Result.Success(entitiesResponse, "Entities retrieved successfully");
    }

    public async Task<Result<TDto>> GetByIdAsync(int id)
    {
        TEntity entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return Result.NotFound("Entity not found");

        var entityResponse = _mapper.Map<TDto>(entity);

        return Result.Success(entityResponse, "Entity retrieved successfully");
    }
}
