using Ardalis.Specification;

namespace QuickTest.Infrastructure.Interfaces;
public interface IAsyncRepository<T>
       where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<T> AddAsync(T entity);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<int> CountAsync(ISpecification<T> spec);

    Task<int> CountAsync();

    Task<T> FirstAsync(ISpecification<T> spec);

    Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
}
