using Application.Interfaces;
using Application.Interfaces.Persistence;
using AutoMapper;

namespace Application.Services;

public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public Task AddAsync(TDto dto)
    {
        var entityDto = _mapper.Map<TEntity>(dto);

        return _unitOfWork.Repository<TEntity>().AddAsync(entityDto);
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
