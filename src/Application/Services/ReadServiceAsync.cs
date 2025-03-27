using System.Linq.Expressions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistence;
using Ardalis.Result;
using AutoMapper;
using Domain.Common.Constants;

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
        IEnumerable<TEntity> entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();

        var entitiesResponse = _mapper.Map<IEnumerable<TDto>>(entities);

        if (entities == null)
            return Result.NotFound(ReplyMessages.Error.NotFound);

        return Result.Success(entitiesResponse, ReplyMessages.Success.Query);
    }

    public async Task<Result<TDto>> GetByIdAsync(int id)
    {
        TEntity? entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);

        if (entity == null)
            return Result.NotFound(ReplyMessages.Error.NotFound);

        var entityResponse = _mapper.Map<TDto>(entity);

        return Result.Success(entityResponse, ReplyMessages.Success.Query);
    }

    public async Task<Result<IEnumerable<TDto>>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();
        var entitiesResponse = _mapper.Map<IEnumerable<TDto>>(entities);

        if (entities == null)
            return Result.NotFound(ReplyMessages.Error.NotFound);

        return Result.Success(entitiesResponse, ReplyMessages.Success.Query);
    }
}
