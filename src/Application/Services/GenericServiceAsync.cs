using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
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
        var entity = _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
        var entityDto = _mapper.Map<TEntity>(entity);
        return _unitOfWork.Repository<TEntity>().DeleteAsync(entityDto);
    }

    public Task UpdateAsync(TDto dto)
    {
        throw new NotImplementedException();
    }
}
