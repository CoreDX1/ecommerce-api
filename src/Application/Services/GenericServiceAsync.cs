using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Ardalis.Result;
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

    public async Task<Result<TDto>> AddAsync<T>(T dto)
    {
        var entityDto = _mapper.Map<TEntity>(dto);

        await _unitOfWork.Repository<TEntity>().AddAsync(entityDto);

        return Result.Success();
    }

    public Task DeleteAsync(int id)
    {
        var entity = _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
        var entityDto = _mapper.Map<TEntity>(entity);
        return _unitOfWork.Repository<TEntity>().DeleteAsync(entityDto);
    }

    public async Task UpdateAsync(TDto dto)
    {
        var entityDto = _mapper.Map<TEntity>(dto);
        await _unitOfWork.Repository<TEntity>().UpdateAsync(entityDto);
    }

    public async Task UpdateAsync<T>(T dto)
    {
        var entityDto = _mapper.Map<TEntity>(dto);
        await _unitOfWork.Repository<TEntity>().UpdateAsync(entityDto);
    }
}
