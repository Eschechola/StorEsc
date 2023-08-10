using Microsoft.EntityFrameworkCore;
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
        string sellerId = "",
        string name = "",
        decimal minimumPrice = 0,
        decimal maximumPrice = 1_000_000)
    {
        var query = _dbSet as IQueryable<Product>;

        query = FilterProductsBySellerId(query, sellerId);
        query = FilterProductsByNameOrDescription(query, name);
        query = FilterProductsByPrice(query, minimumPrice, maximumPrice);

        return await query.AsNoTracking()
            .OrderBy(entity => entity.Name)
            .ToListAsync();
    }

    private IQueryable<Product> FilterProductsBySellerId(IQueryable<Product> query, string sellerId)
        => string.IsNullOrEmpty(sellerId)
            ? query
            : query.Where(entity => entity.SellerId.Equals(Guid.Parse(sellerId)));
    
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