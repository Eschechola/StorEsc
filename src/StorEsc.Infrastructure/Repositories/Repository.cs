using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;

namespace StorEsc.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    private readonly StorEscContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(StorEscContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public abstract IUnitOfWork UnitOfWork { get; }

    public void Create(T entity)
        => _dbSet.Add(entity);

    public void Update(T entity)
        => _context.Entry(entity).State = EntityState.Modified;

    public async void Remove(Guid id)
        => _dbSet.Remove(await GetByIdAsync(id));

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> query)
        => await _dbSet.AnyAsync(query);

    public async Task<int> CountAsync(Expression<Func<T, bool>> query = null)
        => query == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(query);

    public async Task<T> GetByIdAsync(string id)
        => await GetAsync(entity => entity.Id.ToString() == id);

    public async Task<bool> ExistsByIdAsync(string id)
        => await ExistsAsync(entity => entity.Id.ToString() == id);

    public async Task<T> GetByIdAsync(Guid id)
        => await GetAsync(entity => entity.Id == id);

    public async Task<T> GetAsync(
        Expression<Func<T, bool>> query = null,
        string includeProperties = "",
        bool noTracking = true)
        => noTracking
            ? await BuildQuery(query, includeProperties).AsNoTracking().FirstOrDefaultAsync()
            : await BuildQuery(query, includeProperties).FirstOrDefaultAsync();

    public async Task<IList<T>> GetAllAsync(
        Expression<Func<T, bool>> query = null,
        string includeProperties = "",
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        bool noTracking = true,
        int? itensCount = null)
        => noTracking
            ? await BuildQuery(query, includeProperties, orderBy, itensCount).AsNoTracking().ToListAsync()
            : await BuildQuery(query, includeProperties, orderBy, itensCount).ToListAsync();

    private IQueryable<T> BuildQuery(
        Expression<Func<T, bool>> predicate = null,
        string includeProperties = "",
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? itensCount = null)
    {
        var query = _dbSet as IQueryable<T>;

        query = ApplyQueryPredicate(query, predicate);
        query = ApplyQueryIncludedProperties(query, includeProperties);
        query = ApplyOrderBy(query, orderBy);
        query = ApplyQueryItensCount(query, itensCount);

        return query;
    }

    private IQueryable<T> ApplyQueryPredicate(IQueryable<T> query, Expression<Func<T, bool>> predicate)
        => predicate != null
            ? query.Where(predicate)
            : query;

    private IQueryable<T> ApplyQueryIncludedProperties(IQueryable<T> query, string includeProperties)
    {
        if (string.IsNullOrEmpty(includeProperties) is false)
            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

        return query;
    }

    private IQueryable<T> ApplyQueryItensCount(IQueryable<T> query, int? itensCount)
    {
        if (itensCount.HasValue)
            query = query.Take(itensCount.Value);

        return query;
    }

    private IQueryable<T> ApplyOrderBy(IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
    {
        if (orderBy != null)
            query = orderBy(query);

        return query;
    }

    public virtual void Dispose()
        => _context?.Dispose();
}