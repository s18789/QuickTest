using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Data;
public class BaseRepository<T> : IAsyncRepository<T>
    where T : class
{
    protected readonly DataContext context;

    public BaseRepository(DataContext dbContext)
    {
        this.context = dbContext;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var keyValues = new object[] { id };
        return await this.context.Set<T>().FindAsync(keyValues);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await this.context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        var specificationResult = this.ApplySpecification(spec);
        return await specificationResult.ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var specificationResult = this.ApplySpecification(spec);
        return await specificationResult.CountAsync();
    }

    public async Task<int> CountAsync()
    {
        return await this.CountAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var z = await this.context.Set<T>().AddAsync(entity);
        await this.context.SaveChangesAsync();

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
    {
        await this.context.Set<T>().AddRangeAsync(entity);
        await this.context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        this.context.Set<T>().Update(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        this.context.Set<T>().Remove(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task<T> FirstAsync(ISpecification<T> spec)
    {
        var specificationResult = this.ApplySpecification(spec);
        return await specificationResult.FirstAsync();
    }

    public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
    {
        var specificationResult = this.ApplySpecification(spec);
        return await specificationResult.FirstOrDefaultAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var evaluator = new SpecificationEvaluator();
        return evaluator.GetQuery(this.context.Set<T>().AsQueryable(), spec);
    }
}
