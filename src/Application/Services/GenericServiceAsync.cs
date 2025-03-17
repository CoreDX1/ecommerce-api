using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services;

public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericServiceAsync(IGenericRepository<TEntity> repository, IMapper mapper)
        : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task AddAsync(TDto dto)
    {
        var entityDto = _mapper.Map<TEntity>(dto);

        return _repository.AddAsync(entityDto);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TDto dto)
    {
        throw new NotImplementedException();
    }
}
