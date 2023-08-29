using Microsoft.EntityFrameworkCore;
using StorEsc.Core.Enums;
using StorEsc.Domain.Entities;
using StorEsc.Infrastructure.Context;
using StorEsc.Infrastructure.Interfaces;
using StorEsc.Infrastructure.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.EF;

namespace StorEsc.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly StorEscContext _context;

    public ProductRepository(StorEscContext context) : base(context)
    {
        _context = context;
    }

    public override IUnitOfWork UnitOfWork => _context;

    public async Task<IList<Product>> SearchProductsAsync(
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000,
        OrderBy orderBy = OrderBy.CreatedAtDescending)
    {
        var query = _dbSet as IQueryable<Product>;

        query = FilterProductsByNameOrDescription(query, name);
        query = FilterProductsByPrice(query, minimumPrice, maximumPrice);
        query = OrderQuery(query, orderBy);
        
        return await query
            .Where(entity => entity.Enabled)
            .AsNoTracking()
            .ToListAsync();
    }

    private IQueryable<Product> OrderQuery(IQueryable<Product> query, OrderBy orderBy)
    {
        switch (orderBy)
        {
            case OrderBy.NameAscending:
                return query.OrderBy(entity => entity.Name);
            
            case OrderBy.NameDescending:
                return query.OrderByDescending(entity => entity.Name);
            
            case OrderBy.CreatedAtAscending:
                return query.OrderBy(entity => entity.CreatedAt);
            
            case OrderBy.CreatedAtDescending:
                return query.OrderByDescending(entity => entity.CreatedAt);
            
            default:
                return query.OrderByDescending(entity => entity.CreatedAt);
        }
    }
    
    private IQueryable<Product> FilterProductsByNameOrDescription(IQueryable<Product> query, string name)
        => string.IsNullOrEmpty(name)
            ? query
            : query.Where(entity => Functions.FreeText(entity.Name, name) || Functions.FreeText(entity.Description, name));
    
    private IQueryable<Product> FilterProductsByPrice(
        IQueryable<Product> query,
        decimal minimumPrice = 0,
        decimal maximumPrice = Decimal.MaxValue)
        => minimumPrice < 0
            ? query
            : query.Where(entity => entity.Price >= minimumPrice && entity.Price <= maximumPrice);
}