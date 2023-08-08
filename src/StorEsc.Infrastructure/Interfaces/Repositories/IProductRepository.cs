using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IList<Product>> SearchProductsAsync(
        string sellerId,
        string name,
        string description,
        decimal minimumPrice = 0,
        decimal maximumPrice = Decimal.MaxValue);
}