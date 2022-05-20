using System.Linq.Expressions;
using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }

    void Create(T entity);
    void Update(T entity);
    void Remove(Guid id);
    Task<T> FindByIdAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> query);
    Task<int> CountAsync(Expression<Func<T, bool>> query = null);
    Task<T> GetAsync(
        Expression<Func<T, bool>> query = null,
        string includeProperties = "",
        bool noTracking = true);
    Task<IList<T>> GetAllAsync(
        Expression<Func<T, bool>> query = null,
        string includeProperties = "",
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        bool noTracking = true,
        int? itensCount = null);
}