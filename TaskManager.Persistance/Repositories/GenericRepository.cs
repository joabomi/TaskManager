using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Models.Persistance;
using TaskManager.Domain.Common;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly TaskManagerDatabaseContext _context;

    public GenericRepository(TaskManagerDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task<PagedResult<T>> GetPagedAsync(IQueryable<T> baseQuery, BaseQueryParameters parameters)
    {
        var totalCount = await baseQuery.CountAsync();

        if (!string.IsNullOrEmpty(parameters.SortBy))
        {
            var property = typeof(T).GetProperty(parameters.SortBy);
            if (property != null)
            {
                var ordering = parameters.SortDescending ? "descending" : "ascending";
                baseQuery = System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(baseQuery, $"{parameters.SortBy} {ordering}");
            }
            else
            {
                throw new BadRequestException($"Property '{parameters.SortBy}' does not exist on type '{typeof(T).Name}'. Valid properties are: {string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}.");

            }
        }

        var items = await baseQuery
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }
}