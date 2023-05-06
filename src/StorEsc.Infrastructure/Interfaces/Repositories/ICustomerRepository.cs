using StorEsc.Domain.Entities;

namespace StorEsc.Infrastructure.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> GetByIdAsync(string id);
    Task<Customer> GetByEmailAsync(string email, string includeProperties = "");
    Task<bool> ExistsByEmailAsync(string email);
}