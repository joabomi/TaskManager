using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.Common;
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
    public async Task<PagedResult<T>> GetPagedAsync(IQueryable<T> baseQuery, BaseQuery featureQuery)
    {
        var totalCount = await baseQuery.CountAsync();

        if (!string.IsNullOrEmpty(featureQuery.SortBy))
        {
            var property = typeof(T).GetProperty(featureQuery.SortBy);
            if (property != null)
            {
                var ordering = featureQuery.SortDescending ? "descending" : "ascending";
                baseQuery = System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(baseQuery, $"{featureQuery.SortBy} {ordering}");
            }
            else
            {
                throw new BadRequestException($"Property '{featureQuery.SortBy}' does not exist on type '{typeof(T).Name}'. Valid properties are: {string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}.");

            }
        }

        var items = await baseQuery
            .Skip((featureQuery.PageNumber - 1) * featureQuery.PageSize)
            .Take(featureQuery.PageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = featureQuery.PageNumber,
            PageSize = featureQuery.PageSize
        };
    }
}