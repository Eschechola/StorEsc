using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Interfaces.Repositories;

public interface ISellerRepository : IRepository<Seller>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<Seller> GetByEmailAsync(string email, string includeProperties = "");
}