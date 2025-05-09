using TaskManager.Application.Features.Common;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<PagedResult<T>> GetPagedAsync(IQueryable<T> baseQuery, BaseQuery parameters);
    Task<T> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
